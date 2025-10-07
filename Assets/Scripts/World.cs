using System.Linq;
using UnityEngine;

public class World : MonoBehaviour
{
    public GameObject ActorPrefab;
    public Transform EntityContainer;
    
    public int MainActorId = -1;
    
    public int Width = 10;
    public int Height = 10;
    public bool[,] Walkable;
    
    private void Awake()
    {
        Walkable = new bool[Width, Height];
        
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (x > 0 && x < Width - 1 && y > 0 && y < Height - 1)
                {
                    Walkable[x, y] = true;
                }
                else
                {
                    Walkable[x, y] = false;
                }
            }
        }

        Walkable[5, 5] = false;
    }
    
    private void OnDrawGizmos()
    {
        if (Walkable == null)
            return;

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Vector3 pos = new Vector3(x, -y, 0); // Cada tile ocupa 1 unidad
                Vector3 offset = new Vector3(0.5f, -0.5f, 0); // Centrar el gizmo en la celda

                Gizmos.color = Walkable[x, y] ? new Color(0, 1, 0, 0.3f) : new Color(1, 0, 0, 0.3f);
                Gizmos.DrawCube(pos + offset, Vector3.one);

                // Opcional: dibuja el borde de la celda
                //Gizmos.color = Color.black;
                //Gizmos.DrawWireCube(pos + offset, Vector3.one);
            }
        }
    }
    
    public Actor CreateActor(ActorCreationData data)
    {
        GameObject instance = Instantiate(ActorPrefab, EntityContainer);
        instance.transform.position = new Vector2(data.GridPosition.x, -data.GridPosition.y);

        Actor actor = instance.GetComponent<Actor>();
        actor.Id = data.Id;
        actor.Name = data.Name;
        actor.GridPosition = data.GridPosition;

        return actor;
    }
    
    public Actor GetMainActor()
    {
        return MainActorId == -1 ? 
            null : GetActor(MainActorId);
    }
    
    public Actor GetActorAt(Vector2Int pos)
    {
        return EntityContainer
            .GetComponentsInChildren<Actor>()
            .FirstOrDefault(actor => actor.GridPosition == pos);
    }
    
    public Actor GetActor(int id)
    {
        return EntityContainer
            .GetComponentsInChildren<Actor>()
            .FirstOrDefault(actor => actor.Id == id);
    }
    
    public void RemoveActor(int id)
    {
        Actor actor = GetActor(id);
        if (actor != null)
        {
            Destroy(actor.gameObject);
        }
    }
    
    public bool IsWalkable(Vector2Int pos)
    {
        if(GetActorAt(pos) != null)
            return false;
          
        if (pos.x < 0 || pos.x >= Width || pos.y < 0 || pos.y >= Height)
            return false;

        return Walkable[pos.x, pos.y];
    }
}
