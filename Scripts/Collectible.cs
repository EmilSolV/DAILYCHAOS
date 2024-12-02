using Godot;
using static System.Formats.Asn1.AsnWriter;

public partial class Collectible : Area2D
{
    [Export] public int Points; // Cambia este valor según el tipo de objeto.
    [Export] public string Group { get; set; } // Ejemplo: "Trabajo", "Salud", etc.
    public string SubType { get; set; } // Ejemplo: "Tareas", "Reuniones"

    private StageManagerScript stageManager;    

    public override void _Ready()
    {
        // Conectar la señal para detectar al jugador.
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("Collectibles");

        // Emitir una señal global cuando se instancia.
        GetTree().CallGroup("GameManagers", "OnCollectibleInstantiated", this);

        var stageManagers = GetTree().GetNodesInGroup("StageManager");
        if (stageManagers.Count > 0)
            stageManager = GetTree().GetNodesInGroup("StageManager")[0] as StageManagerScript;       
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            var gameManager = GetTree().GetNodesInGroup("GameManagers")[0] as GameManager;

            if (gameManager != null)
            {
                gameManager.UpdateHealth(Points);

                if (Group == stageManager.CurrentStageGroup)
                {
                    gameManager.UpdatePoints(Points);
                    GD.Print("¡Puntos sumados! Score: " + Points);
                }
                else
                {
                    gameManager.UpdatePoints(-Points);
                    GD.Print("Penalización. Score: " + Points);
                }
            }
            QueueFree();           
        }
    }
}