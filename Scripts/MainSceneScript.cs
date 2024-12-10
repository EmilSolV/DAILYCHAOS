using Godot;

public partial class MainSceneScript : Node2D
{
    private PackedScene pauseMenuScene;
    private AudioStreamPlayer audioPlayer;
    private Control pauseMenu;

    private void InitializeAudioPlayer()
    {
        //FindAudioPlayerByName("Background").Play();
        AddToGroup("MainSceneScript");
    }

    public override void _Ready()
    {
        InitializeAudioPlayer();
        pauseMenu = GetNode<Control>("PauseMenu");
        pauseMenu.Visible = false;

    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("Pause"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GetTree().Paused)
        {
            // Reanudar el juego
            GetTree().Paused = false;
            pauseMenu.Visible = false;
        }
        else
        {
            // Pausar el juego
            GetTree().Paused = true;
            pauseMenu.Visible = true;
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
