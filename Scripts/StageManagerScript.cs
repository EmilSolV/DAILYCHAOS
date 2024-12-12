using Godot;
using System;
using System.Threading.Tasks;

public partial class StageManagerScript : Node
{
    [Export]
    public float WaitTime = 12.0f;
    [Export]
    public int ObjectSpeedUpdate = 60;
    [Export]
    public float SpawnIntervalUpdate = 0.8f;

    public string CurrentStageGroup { get; private set; }

    private int currentStageIndex = 0;
    private readonly string[] stageGroups = { "TRABAJO", "ESTUDIO", "HOGAR", "BIENESTAR" };

    private Timer stageTimer;
    private Label topicLabel;
    private Label topic;
    private int etapa = 1;
    private ObjectSpawner objectSpawner;

    private AudioStreamPlayer audioPlayer;


    public override void _Ready()
    {
        var nombreEtapa = "Etapa" + etapa + "_loop";
        var nombreEtapaFin = "Etapa" + etapa + "_fin";
        PlayStageAudio(nombreEtapa, nombreEtapaFin);

        AddToGroup("StageManager");
        InitializeStageTimer();

        var objectSpawners = GetTree().GetNodesInGroup("ObjectSpawner");
        if (objectSpawners.Count > 0)
            objectSpawner = GetTree().GetNodesInGroup("ObjectSpawner")[0] as ObjectSpawner;

        topicLabel = GetNode<Label>("TopicLabel");
        topic = GetNode<Label>("Topic");

        RandomizeStageGroups();
        UpdateStageAsync();
        objectSpawner.StartSpawning();
    }

    private AudioStreamPlayer FindAudioPlayerByName(string name)
    {
        var audioPlayers = GetTree().GetNodesInGroup("AudioStreamPlayer");
        foreach (var player in audioPlayers)
        {
            if (player is AudioStreamPlayer audioPlayer && audioPlayer.Name == name)
            {
                return audioPlayer;
            }
        }
        return null;
    }

    private void InitializeStageTimer()
    {
        stageTimer = new Timer();        
        stageTimer.WaitTime = WaitTime;
        stageTimer.Autostart = true;
        stageTimer.OneShot = false;
        stageTimer.Timeout += AdvanceStage;
        AddChild(stageTimer);
    }

    public void AdvanceStage()
    {
        CurrentStageGroup = stageGroups[currentStageIndex];
        ChangeNodeVisibility(false, CurrentStageGroup);
        currentStageIndex = (currentStageIndex + 1) % stageGroups.Length;

        var nombreEtapaAnterior = "Etapa" + etapa + "_loop";
        etapa += 1;

        var nombreEtapa = "Etapa" + etapa + "_loop";
        var nombreEtapaFin = "Etapa" + etapa + "_fin";

        FindAudioPlayerByName(nombreEtapaAnterior).Stop();
        PlayStageAudio(nombreEtapa, nombreEtapaFin);
        UpdateObjectSpawnerVelocity();
        UpdateStageAsync();
    }

    private async void PlayStageAudio(string nombreEtapa, string nombreEtapaFin)
    {
        var audioPlayer = FindAudioPlayerByName(nombreEtapa);
        var audioPlayerFin = FindAudioPlayerByName(nombreEtapaFin);

        audioPlayer.Play();
        await ToSignal(audioPlayer, "finished");
        audioPlayerFin.Play();
        await ToSignal(audioPlayerFin, "finished");
    }

    private async Task UpdateStageAsync()
    {
        CurrentStageGroup = stageGroups[currentStageIndex];
        ChangeNodeVisibility(true, CurrentStageGroup);
        await UpdateTopicLabelAsync();        
    }

    private void ChangeNodeVisibility(bool visible, string nodeName)
    {
        var stageNode = GetNode<Node>("Stages");
        if (stageNode != null)
        {
            var node = stageNode.GetNode<Node2D>(nodeName);
            if (node != null)
            {
                node.Visible = visible;
            }
        }
    }

    private async Task UpdateTopicLabelAsync()
    {
        topic.Text = CurrentStageGroup;

        topic.Visible = true;
        topicLabel.Visible = true;
        await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
        topic.Visible = false;
        topicLabel.Visible = false;


    }

    private void RandomizeStageGroups()
    {
        Random rng = new Random();
        int n = stageGroups.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            string value = stageGroups[k];
            stageGroups[k] = stageGroups[n];
            stageGroups[n] = value;
        }
    }

    private void UpdateObjectSpawnerVelocity()
    {
        if (objectSpawner != null)
        {
            objectSpawner.ObjectSpeed += ObjectSpeedUpdate;
            objectSpawner.SpawnInterval -= SpawnIntervalUpdate;
        }
    }
}
