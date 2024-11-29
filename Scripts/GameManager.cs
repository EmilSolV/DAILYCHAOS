using Godot;
using System;

public partial class GameManager : Node
{
    [Export] public int PlayerHealth = 100; // Salud inicial del jugador.

    public override void _Ready()
    {
        // Conectar la señal collected de todos los objetos Collectible en la escena.
        foreach (Collectible collectible in GetTree().GetNodesInGroup("Collectibles"))
        {
            collectible.Connect("collected", new Callable(this, nameof(OnCollectibleCollected)));
        }
    }

    private void OnCollectibleCollected(int points)
    {
        // Disminuir la salud del jugador.
        if (points > 0)
        {
            PlayerHealth = Math.Min(PlayerHealth + points, 100); // Limitar la salud máxima a 100.
        }
        else
        {
            PlayerHealth += points; // Disminuir la salud si los puntos son negativos.
        }
        GD.Print("Salud del jugador: " + PlayerHealth);

        // Verificar si la salud del jugador es menor o igual a 0.
        if (PlayerHealth <= 0)
        {
            GD.Print("El jugador ha muerto.");
            // Aquí puedes agregar lógica adicional para manejar la muerte del jugador.
        }
    }
}