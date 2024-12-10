using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectSpawner : Node2D
{
    [Export] public Godot.Collections.Array<PackedScene> PositiveObjects = new Godot.Collections.Array<PackedScene>();
    [Export] public float SpawnInterval = 0.5f;
    [Export] public float ObjectSpeed = 200f;
    [Export] public int xStart = -466;
    [Export] public int xEnd = 485;
    [Export] public float spawnY = -300;

    private Random _random = new Random();
    private Vector2 screenSize;

    private Control gameOverMenu;
    private Label pointsLabel;
    private Label messageLabel;
    private Button continueButton;
    private int playerPoints;

    public override void _Ready()
    {
        AddToGroup("ObjectSpawner");

        screenSize = GetViewport().GetVisibleRect().Size;
    }

    public void StartSpawning()
    {
        Timer spawnTimer = new Timer();
        AddChild(spawnTimer);
        spawnTimer.WaitTime = SpawnInterval;
        spawnTimer.OneShot = false;
        spawnTimer.Connect("timeout", Callable.From(SpawnObject));
        spawnTimer.Start();
    }

    private void SpawnObject()
    {
        PackedScene objectToSpawn = null;

        if (_random.Next(2) == 0 && PositiveObjects.Count > 0)
        {
            objectToSpawn = PositiveObjects[_random.Next(PositiveObjects.Count)];
        }

        if (objectToSpawn != null)
        {
            Node2D instance = (Node2D)objectToSpawn.Instantiate();            

            float spawnX = _random.Next(xStart, xEnd);
            instance.ZIndex = 1;
            instance.Position = new Vector2(spawnX, spawnY);
            AddChild(instance);

            var script = instance.GetScript();

            var moveDownInstance = instance.GetNode<MoveDown>(".");
            if (moveDownInstance != null)
            {
                moveDownInstance.Speed = ObjectSpeed;
            }
        }
    }
}