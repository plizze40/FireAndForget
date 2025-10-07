using UnityEngine;

public class Actor : MonoBehaviour
{
    private Vector2 _targetPosition;
    public float MoveSpeed { get; set; } = 5f;
    public int Id { get; set; }
    public string Name { get; set; }
    
    public Vector2Int GridPosition { get; set; }
    public bool IsMoving { get; private set; }


    public void MoveTo(Vector2 offset)
    {
        _targetPosition = (Vector2)transform.position + offset;
        IsMoving = true;
    }

    private void Update()
    {
        if (IsMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, _targetPosition, MoveSpeed * Time.deltaTime);
            if ((Vector2)transform.position == _targetPosition)
            {
                transform.position = _targetPosition;
                IsMoving = false;
            }
        }
    }
}