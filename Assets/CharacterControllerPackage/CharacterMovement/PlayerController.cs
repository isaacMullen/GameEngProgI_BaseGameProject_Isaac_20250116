using UnityEngine.Tilemaps;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private Vector3 moveVector = Vector2.zero;
    private Rigidbody2D rb;

    private Bounds mapBoundary;
    public Tilemap map;   

    private void Start()
    {
        mapBoundary = map.localBounds;
        mapBoundary.size *= 0.8f; //shrinking bounds to better represent the visible tilemap
        rb = GetComponent<Rigidbody2D>();
    }   

    // Subbing and unsubbing
    private void OnEnable()
    {
        Actions.MoveEvent += UpdateMoveVector;
    }

    private void OnDisable()
    {
        Actions.MoveEvent -= UpdateMoveVector;
    }

    private void UpdateMoveVector(Vector2 InputVector)
    {
        moveVector = InputVector;
    }

    private void FixedUpdate()
    {       
        Vector2 newPosition = transform.position + speed * Time.fixedDeltaTime * moveVector;

        //check if the new position is outside the map's bounds
        //left side was not lining up with the visible tilemap. hard coded a small offset to fix it.
        if (newPosition.x < mapBoundary.min.x - 2)
        {
            newPosition.x = mapBoundary.min.x - 2; 
        }
        if (newPosition.x > mapBoundary.max.x)
        {
            newPosition.x = mapBoundary.max.x;  
        }
        if (newPosition.y < mapBoundary.min.y)
        {
            newPosition.y = mapBoundary.min.y;  
        }
        if (newPosition.y > mapBoundary.max.y)
        {
            newPosition.y = mapBoundary.max.y;  
        }

        // Apply the adjusted position
        transform.position = newPosition;          
    }
}
