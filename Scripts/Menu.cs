using Godot;
using System;

public partial class Menu : Control
{
    // Called when the node enters the scene tree for the first time.
    private AudioStreamPlayer audioPlayer;
    public override void _Ready()
    {
        InitializeAudioPlayer();

    }

    private void InitializeAudioPlayer()
    {
        var audioPlayers = GetTree().GetNodesInGroup("AudioStreamPlayer");
        if (audioPlayers.Count > 0)
            audioPlayer = GetTree().GetNodesInGroup("AudioStreamPlayer")[0] as AudioStreamPlayer;

        audioPlayer.Play();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

    private void _on_StartButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
    }

    private void _on_CreditsButton_pressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Credits.tscn");
    }

    private void _on_QuitButton_pressed()
    {
        GetTree().Quit();
    }
}
