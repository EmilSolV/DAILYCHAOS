using Godot;

public partial class PauseMenu : Control
{
    // Called when the node enters the scene tree for the first time.

    private MainSceneScript mainScene;

    public override void _Ready()
    {
        
    }

    private void InitializeAudioPlayer()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    //public override void _Process(double delta)
    //{
    //    if (Input.IsActionJustPressed("Pause"))
    //    {
    //        _on_continue_button_pressed();
    //    }
    //}

    private void _on_continue_button_pressed()
    {
        var mainScenes = GetTree().GetNodesInGroup("MainSceneScript");
        if (mainScenes.Count > 0)
            mainScene = GetTree().GetNodesInGroup("MainSceneScript")[0] as MainSceneScript;
        mainScene.TogglePause();
    }

    private void _on_menu_button_pressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/Menu.tscn");
    }

    private void _on_RetryButton_pressed()
    {
        GetTree().Paused = false;
        GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
    }
}
