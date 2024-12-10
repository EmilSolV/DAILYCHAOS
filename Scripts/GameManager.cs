using Godot;
using System;

public partial class GameManager : Node
{
    [Export] public int PlayerPoints = 50;

    private ScorePoints scorePointsLabel;

    private TimerLabel timerLabel;

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

            var timerLabels = GetTree().GetNodesInGroup("Timer");
            if (timerLabels.Count > 0)
                timerLabel = GetTree().GetNodesInGroup("Timer")[0] as TimerLabel;

            timerLabel.EndTimer();
        }
        else
        {
            UpdatePointsDisplay();
        }
    }

    public void UpdatePointsDisplay()
    {
        scorePointsLabel.Text = PlayerPoints.ToString();
    }
}