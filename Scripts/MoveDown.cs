using Godot;

public partial class MoveDown : Node2D
{
    public float Speed { get; set; } = 200f;

    public override void _Process(double delta)
    {
        // Mueve el objeto hacia abajo (en el eje Y) cada frame.
        Position += new Vector2(0, Speed * (float)delta);

        // Elimina el objeto cuando llega a la parte inferior de la pantalla.
        if (Position.Y > GetViewport().GetVisibleRect().Size.Y)
        {
            QueueFree();
        }
    }
}
