using Godot;

public partial class GlobalAudioPlayer : Node
{
    private static GlobalAudioPlayer _instance;
    private AudioStreamPlayer _audioPlayer;

    public override void _Ready()
    {
        if (_instance == null)
        {
            _instance = this;
            _audioPlayer = new AudioStreamPlayer();
            AddChild(_audioPlayer);
            _audioPlayer.Stream = GD.Load<AudioStream>("res://path/to/your/audio/file.ogg");
            _audioPlayer.Play();

            SetProcess(false);
            GetTree().Root.AddChild(this);
        }
        else
        {
            QueueFree();
        }
    }

    public static void Play()
    {
        _instance?._audioPlayer.Play();
    }

    public static void Stop()
    {
        _instance?._audioPlayer.Stop();
    }
}
