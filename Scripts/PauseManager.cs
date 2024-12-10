using Godot;
using System;

public partial class PauseManager : Node
{
	// Called when the node enters the scene tree for the first time.

	public static PauseManager Instance { get; private set; }

    //[Signal]
    //public delegate void PauseToggled(bool paused);

    private bool _paused = false;
    public override void _Ready()
	{
        Instance = this;
    }

    public override void _Input(InputEvent @event)
    {
        //if (@event is InputEventKey eventKey)
        //{
        //}

        //if (Input.IsActionJustPressed("Pause"))
        //{
        //    _paused = !_paused;

        //    EmitSignal((nameof(PauseToggled)), _paused);
        //    GetTree().Paused = _paused;
        //}
    }
}
