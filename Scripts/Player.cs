using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200; // Velocidad del jugador

    public override void _PhysicsProcess(double delta)
    {
        // Movimiento basado en las entradas del jugador
        Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed("input_right"))
            velocity.X += 1;
        if (Input.IsActionPressed("input_left"))
            velocity.X -= 1;
        if (Input.IsActionPressed("input_down"))
            velocity.Y += 1;
        if (Input.IsActionPressed("input_up"))
            velocity.Y -= 1;

        // Normalizamos para evitar que diagonales sean más rápidas y escalamos con la velocidad
        velocity = velocity.Normalized() * Speed;

        // Usamos MoveAndSlide para manejar colisiones
        Velocity = velocity;
        MoveAndSlide();
    }
}
