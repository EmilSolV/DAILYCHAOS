using Godot;

public partial class VBoxContainer : Godot.VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        var startbutton = GetNode<Button>("StartButton");
        startbutton.GrabFocus();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
