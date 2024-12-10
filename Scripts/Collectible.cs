﻿using Godot;

public partial class Collectible : Area2D
{
    [Export] public int Points; // Cambia este valor según el tipo de objeto.
    [Export] public string Group { get; set; } // Ejemplo: "Trabajo", "Salud", etc.
    public string SubType { get; set; } // Ejemplo: "Tareas", "Reuniones"

    private StageManagerScript stageManager;

    public override void _Ready()
    {
        // Conectar la señal para detectar al jugador.
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("Collectibles");

        // Emitir una señal global cuando se instancia.
        GetTree().CallGroup("GameManagers", "OnCollectibleInstantiated", this);

        var stageManagers = GetTree().GetNodesInGroup("StageManager");
        if (stageManagers.Count > 0)
            stageManager = GetTree().GetNodesInGroup("StageManager")[0] as StageManagerScript;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            var gameManager = GetTree().GetNodesInGroup("GameManagers")[0] as GameManager;

            if (gameManager != null)
            {
                if (Group == stageManager.CurrentStageGroup)
                {
                    gameManager.UpdatePoints(Points);
                    PlaySound("GoodSound");
                }
                else
                {
                    gameManager.UpdatePoints(-Points);
                    PlaySound("BadSound");
                }
            }
            QueueFree();
        }
    }

    private void PlaySound(string soundName)
    {
        var audioPlayer = FindAudioPlayerByName(soundName);
        if (audioPlayer != null)
        {
            audioPlayer.Play();
        }
    }

    private AudioStreamPlayer FindAudioPlayerByName(string name)
    {
        var audioPlayers = GetTree().GetNodesInGroup("AudioStreamPlayer");
        foreach (var player in audioPlayers)
        {
            if (player is AudioStreamPlayer audioPlayer && audioPlayer.Name == name)
            {
                audioPlayer.VolumeDb = +10;
                return audioPlayer;
            }
        }
        return null;
    }
}
