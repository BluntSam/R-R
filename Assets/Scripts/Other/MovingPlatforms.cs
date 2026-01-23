using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{   
    //variables 
    public Transform pointA;
    public Transform pointB;
    [SerializeField] private float moveSpeed = 2f;
    private Vector3 nextPosition;
    private const string PLAYER_TAG = "Player";

    void Awake()
    { //setting next position to point b 
        nextPosition = pointB.position;
    }
    
    void FixedUpdate()
    {
        //will change move platforms and change the next position based on where platform is 
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed* Time.deltaTime);

        if (transform.position == nextPosition)
        {
            nextPosition = (nextPosition == pointA.position) ? pointB.position : pointA.position;
        }

    }
  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(PLAYER_TAG))
        {
            collision.gameObject.transform.parent = null;
        }
    }
    
}
