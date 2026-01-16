using UnityEngine;
using System.Collections;

public class smoothCamera : MonoBehaviour
{
    //Components
    private CameraStats stats; //This is just for smooth time for smooth dampening
    [SerializeField] private GameObject player; //Tje Player

    //Field Variables
    private Vector3 posCurrent; //The Current Position of the Camera
    private Vector3 posTarget; //The Current Position of the Target (The Player)
    private Vector3 camVelocity; //The Velocity of the Camera's Movement

    private void Awake()
    {
        //Initialization
        stats = new CameraStats();
        camVelocity = new Vector3(0, 0, 2);
        stats.SmoothTime = 0.5f;
    }

    private void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        posCurrent = transform.position;
        posTarget = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        transform.position = Vector3.SmoothDamp(posCurrent, posTarget, ref camVelocity, stats.SmoothTime);
    }
}