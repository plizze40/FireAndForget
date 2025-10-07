using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public World World;

    private Actor _mainActor;
    
    void LateUpdate()
    {
        _mainActor ??= World.GetMainActor(); 

        if (_mainActor != null)
        {
            Vector3 targetPosition = _mainActor.transform.position;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, transform.position.z);
        }
    }
}
