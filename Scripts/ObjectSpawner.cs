using Godot;
using System;
using System.Collections.Generic;

public partial class ObjectSpawner : Node2D
{
    [Export] public Godot.Collections.Array<PackedScene> PositiveObjects = new Godot.Collections.Array<PackedScene>();
    [Export] public Godot.Collections.Array<PackedScene> NegativeObjects = new Godot.Collections.Array<PackedScene>();
    [Export] public float SpawnInterval = 1.5f;
    [Export] public float ObjectSpeed = 200f;

    private Random _random = new Random();
    private Vector2 screenSize;

    public override void _Ready()
    {
        screenSize = GetViewport().GetVisibleRect().Size;
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
        else if (NegativeObjects.Count > 0)
        {
            objectToSpawn = NegativeObjects[_random.Next(NegativeObjects.Count)];
        }

        if (objectToSpawn != null)
        {
            Node2D instance = (Node2D)objectToSpawn.Instantiate();

            float spawnX = _random.Next(0, (int)screenSize.X);
            float spawnY = -50;
            instance.ZIndex = 1;
            instance.Position = new Vector2(spawnX, spawnY);
            AddChild(instance);

            GD.Print("Scripts adjuntos al objeto instanciado:");
            var script = instance.GetScript();
            GD.Print(script);

            var moveDownInstance = instance.GetNode<MoveDown>(".");
            if (moveDownInstance != null)
            {
                moveDownInstance.Speed = ObjectSpeed;
            }
        }
    }
}