using Godot;

public partial class Collectible : Area2D
{
    [Export] public int Points = 10; // Cambia este valor según el tipo de objeto.

    public override void _Ready()
    {
        // Conectar la señal para detectar al jugador.
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("Collectibles");
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is CharacterBody2D player)
        {
            EmitSignal("collected", Points); // Envía los puntos al GameManager.
            QueueFree(); // Elimina el objeto de la escena.
        }
    }
}
