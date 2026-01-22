using UnityEditor.SettingsManagement;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{   
    //variables 
    public Transform pointA;
    public Transform pointB;
    private float moveSpeed = 2f;

    private Vector3 nextPosition;
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
}
