using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public World World;
    private Actor _mainActor;

    void Start()
    {
        Connection.Singleton.MessageReceived += OnMessageReceived; 
    }

    private void OnMessageReceived(string rawData)
    {
        MessageBase message = JsonConvert.DeserializeObject<MessageBase>(rawData);
        
        if (message.Type == MessageType.CreateActor)
        {
            CreateActorMessage data = JsonConvert.DeserializeObject<CreateActorMessage>(rawData);

            World.CreateActor(new ActorCreationData
            {
                Id = data.Id,
                Name = data.Name,
                GridPosition = new Vector2Int(data.X, data.Y)
            });
        }
        else if (message.Type == MessageType.DeleteActor)
        {
            DeleteActorMessage data = JsonConvert.DeserializeObject<DeleteActorMessage>(rawData);
            World.RemoveActor(data.Id);
        }
        else if (message.Type == MessageType.SetMainActor)
        {
            SetMainActorMessage data = JsonConvert.DeserializeObject<SetMainActorMessage>(rawData);
            World.MainActorId = data.Id;
            _mainActor = World.GetMainActor();
        }
    }
    
    private List<Vector2> _movements = new()
    {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right
    };

    private Dictionary<Key, Vector2> _keyToMovement = new()
    {
        { Key.W, Vector2.up },
        { Key.S, Vector2.down },
        { Key.A, Vector2.left },
        { Key.D, Vector2.right }
    };

    void Update()
    {  
        if (_mainActor && !_mainActor.IsMoving)
        {
            foreach (var entry in _keyToMovement)
            {
                if (Keyboard.current[entry.Key].isPressed)
                {
                    var movement = entry.Value;
                    var gridPosition = _mainActor.GridPosition + Vector2Int.RoundToInt(new Vector2(movement.x, -movement.y));

                    if (World.IsWalkable(gridPosition))
                    {
                        _mainActor.GridPosition = gridPosition;
                        _mainActor.MoveTo(movement);
                        return;
                    }
                }
            }
        }
    }
}
