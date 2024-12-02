using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200; // Velocidad del jugador

    private Camera2D camera;
    private Vector2 screenSize;

    public override void _Ready()
    {
        // Obtener el tamaño del Viewport
        camera = GetViewport().GetCamera2D();
        if (camera == null)
        {
            GD.PrintErr("No se encontró Camera2D en el Viewport.");
            return;
        }

        screenSize = GetViewport().GetVisibleRect().Size;
    }

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

        // Obtener la posición y el tamaño de la cámara
        Vector2 cameraPosition = camera.GlobalPosition;
        Vector2 cameraSize = screenSize / camera.Zoom;

        // Calcular los límites de movimiento
        float leftLimit = cameraPosition.X - cameraSize.X / 2;
        float rightLimit = cameraPosition.X + cameraSize.X / 2;
        float topLimit = -40;
        float bottomLimit = 90;

        // Restringir la posición del jugador dentro de los límites definidos
        Vector2 position = Position;
        //position.X = Mathf.Clamp(position.X, leftLimit, rightLimit);
        position.Y = Mathf.Clamp(position.Y, topLimit, bottomLimit);
        Position = position;
    }
}
