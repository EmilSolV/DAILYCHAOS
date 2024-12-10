using Godot;

public partial class TimerLabel : Label
{
    [Export]
    public float TimeLeft = 60.0f; // Public variable to set the timer duration

    private Control gameOverMenu; // Menú de fin de juego
    private Label messageLabel; // Etiqueta para mostrar el mensaje (ganaste/perdiste)
    private Label pointsNumberLabel; // Etiqueta para el número de puntaje
    private Button continueButton; // Botón para continuar
    private Button quitButton; // Botón para salir
    private GameManager gameManager; // Referencia al script GameManager

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        AddToGroup("Timer");
        UpdateTimerText();
    }

    private void InitializeGameOverMenu()
    {
        gameOverMenu = GetNode<Control>("GameOverMenu");
        messageLabel = gameOverMenu.GetNode<Label>("MessageLabel");
        pointsNumberLabel = gameOverMenu.GetNode<Label>("PointsNumberLabel");
        var vContainerBox = gameOverMenu.GetNode("VBoxContainer");
        continueButton = vContainerBox.GetNode<Button>("RetryButton");
        quitButton = vContainerBox.GetNode<Button>("QuitButton");
        gameManager = GetTree().GetNodesInGroup("GameManagers")[0] as GameManager;
                
        continueButton.Pressed += _on_retry_button_pressed;
        quitButton.Pressed += _on_quit_button_pressed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= (float)delta;
            UpdateTimerText();
        }
        else
        {
            TimeLeft = 0;
            UpdateTimerText();
            EndTimer();
            
        }
    }

    public void EndTimer()
    {
        GetTree().Paused = true;

        InitializeGameOverMenu();

        gameOverMenu.Visible = true;

        var points = gameManager.PlayerPoints;

        if (points == 0)
        {
            messageLabel.Text = "¡Perdiste! :( Ponele más onda la prox";
            PlaySound("DEFEAT");
        }
        else
        {
            if (points <= 50)
            {
                messageLabel.Text = "gg, seguro puedes hacerlo mejor la prox :)";
            }
            else if (points > 50 && points < 100)
            {
                messageLabel.Text = "¡Wow! Sos crack";
            }
            else if (points > 100 && points < 200)
            {
                messageLabel.Text = "Sos el más capito! <3";
            }
            else if (points > 200)
            {
                messageLabel.Text = "WINNER WINNER CHICKEN DINNER";
            }
            PlaySound("VICTORY");
        }

        pointsNumberLabel.Text = points.ToString();
    }

    private void PlaySound(string soundName)
    {
        var audioPlayer = FindAudioPlayerByName(soundName);
        if (audioPlayer != null)
        {
            audioPlayer.Play();
        }
    }

    private AudioStreamPlayer FindAudioPlayerByName(string name)
    {
        var audioPlayers = GetTree().GetNodesInGroup("AudioStreamPlayer");
        foreach (var player in audioPlayers)
        {
            if (player is AudioStreamPlayer audioPlayer && audioPlayer.Name == name)
            {
                audioPlayer.VolumeDb = +10;
                return audioPlayer;
            }
        }
        return null;
    }

    private void _on_retry_button_pressed()
    {
        // Reanudar el juego
        GetTree().Paused = false;

        // Ocultar el menú
        gameOverMenu.Visible = false;

        // Reiniciar la escena
        GetTree().ReloadCurrentScene();
    }

    private void _on_quit_button_pressed()
    {
        // Salir del juego
        GetTree().Quit();
    }

    private void UpdateTimerText()
    {
        Text = TimeLeft.ToString("00");
    }
}
