using Godot;

public partial class MainSceneScript : Node2D
{
    private PackedScene pauseMenuScene;
    private Node pauseMenuInstance;
    private AudioStreamPlayer audioPlayer;

    private void InitializeAudioPlayer()
    {
        //FindAudioPlayerByName("Background").Play();
    }

    public override void _Ready()
    {
        InitializeAudioPlayer();
        pauseMenuScene = (PackedScene)GD.Load("res://Scenes/PauseMenu.tscn");
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        if (GetTree().Paused)
        {
            // Reanudar el juego
            GetTree().Paused = false;
            if (pauseMenuInstance != null)
            {
                pauseMenuInstance.QueueFree(); // Eliminar el menú de pausa
                pauseMenuInstance = null;
            }
        }
        else
        {
            // Pausar el juego
            GetTree().Paused = true;
            pauseMenuInstance = pauseMenuScene.Instantiate();
            AddChild(pauseMenuInstance);
        }
    }
    private AudioStreamPlayer FindAudioPlayerByName(string name)
    {
        var audioPlayers = GetTree().GetNodesInGroup("AudioStreamPlayer");
        foreach (var player in audioPlayers)
        {
            if (player is AudioStreamPlayer audioPlayer && audioPlayer.Name == name)
            {
                return audioPlayer;
            }
        }
        return null;
    }
}
