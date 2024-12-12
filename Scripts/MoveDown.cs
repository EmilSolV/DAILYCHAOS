using Godot;

public partial class MoveDown : Node2D
{
    [Export]
    public float Speed { get; set; } = 200f;

    public override void _Process(double delta)
    {
        Position += new Vector2(0, Speed * (float)delta);

        if (Position.Y > GetViewport().GetVisibleRect().Size.Y)
        {
            QueueFree();
        }
    }
}
