using Godot;
using System;

public partial class GameManager : Node
{
    [Export] public int PlayerHealth = 100; // Salud inicial del jugador.
    [Export] public int PlayerPoints = 50; // Salud inicial del jugador.

    private ScorePoints scorePointsLabel;

    public override void _Ready()
    {
        AddToGroup("GameManagers");

        var scorePointsLabels = GetTree().GetNodesInGroup("ScorePoints");
        if (scorePointsLabels.Count > 0)
            scorePointsLabel = GetTree().GetNodesInGroup("ScorePoints")[0] as ScorePoints;
    }
  

    public void UpdatePoints(int point)
    {
        PlayerPoints += point;

        if (PlayerPoints <= 0)
        {
            PlayerPoints = 0;
            UpdatePointsDisplay();
            GameOver();
        }
        else
        {
            UpdatePointsDisplay();
        }
    }

    private void UpdatePointsDisplay()
    {
        GD.Print($"Puntos actuales: {PlayerPoints}");
        // Aquí puedes actualizar tu HUD
        scorePointsLabel.Text = PlayerPoints.ToString();
    }
        private void GameOver()
    {
        GD.Print("¡Juego terminado!");
        // Implementa lógica de finalización o reinicio
    }

    private void UpdateHealthDisplay()
    {
        GD.Print($"Salud actual: {PlayerHealth}");
        // Aquí puedes actualizar tu HUD
    }

    public void UpdateHealth(int point)
    {
        PlayerHealth += point;
        PlayerHealth = Mathf.Clamp(PlayerHealth, 0, 100); // Limita la salud entre 0 y 100
        UpdateHealthDisplay();

        if (PlayerHealth <= 0)
        {
            GameOver();
        }
    }
}