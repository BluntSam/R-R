using System.Collections;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

public class Grapple : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lr;
    DistanceJoint2D dj;
    public LayerMask grappleLayer;
    public bool isGrappling;
    Vector3 mouseWorldPoint;
    public Health playerHealth;
    public int hpdamage = 1;
    public PauseMenu pauseMenu;
    public DeathScreen deathScreen;
    PlayerInputManager playerInputManager;
    public Color yarnColorValid;
    public Color yarnColorInvalid;
    public Color yarnColorGrapple;
    public bool isGrappleValid;
    public Transform grappleOrigin;
    public float maxGrappleRange = 20;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        dj = GetComponent<DistanceJoint2D>();
        playerInputManager = GetComponent<PlayerInputManager>();
        dj.enabled = false;
        lr.enabled = false;
    }


    private void Update()
    {
        lr.SetPosition(0,grappleOrigin.position);
        mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 DisplacementToCursor = mouseWorldPoint - grappleOrigin.position;
        Vector3 GrappleDirection = DisplacementToCursor.normalized;

        RaycastHit2D hit = Physics2D.Raycast(grappleOrigin.position, GrappleDirection, Mathf.Min(DisplacementToCursor.magnitude, maxGrappleRange),grappleLayer);
        isGrappleValid = hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");

        if (isGrappleValid)
        {
            lr.startColor = yarnColorValid;
            lr.endColor = yarnColorInvalid;
            lr.SetPosition(1,hit.point);
        }
        else
        {
            lr.startColor =yarnColorInvalid;
            lr.endColor=yarnColorInvalid;
            lr.SetPosition(1, grappleOrigin.position+GrappleDirection*Mathf.Min(DisplacementToCursor.magnitude, maxGrappleRange));
        }
        if (playerInputManager.IsClicking && !pauseMenu.paused && !deathScreen.isDead && playerHealth.health > 1 && !isGrappling)
        {
            if(isGrappleValid)
            {
                isGrappling = true;

                dj.enabled = true;
                dj.connectedAnchor = hit.point;
            }
            if (!playerInputManager.IsClicking)
            {
                isGrappling = false;
                lr.enabled = false;
                dj.enabled = false;
            }
        }

 }

    // Update is called once per frame
    /*  void Update()
      {
          if(playerInputManager.IsClicking && !pauseMenu.paused && !deathScreen.isDead && playerHealth.health >1 && !isGrappling)
          {
               point =Camera.main.ScreenToWorldPoint(Input.mousePosition);
              if(Physics2D.OverlapCircle(point,0.1f,grappleLayer))
              {
                  StartCoroutine(grappleDamage(hpdamage));

                  isGrappling = true;
                  lr.enabled = true;
                  lr.SetPosition(0,transform.position);
                  lr.SetPosition(1, point);


                  dj.enabled = true;
                  dj.connectedAnchor = point;
              }
          }

          if (!playerInputManager.IsClicking)
          {
              isGrappling= false;
              lr.enabled = false;
              dj.enabled = false;
          }

          if(isGrappling)
          {
              lr.enabled = true;
              lr.SetPosition(0, transform.position);
              lr.SetPosition(1, point);
          }
      }*/

    private IEnumerator grappleDamage(int amount)
    {
        Debug.Log("Player Took Damage From Using Grapple");
        playerHealth.TakeDamageHopps(amount);
        yield return new WaitForSeconds(1f);
    }

}

/*
 *             Debug.Log("Player Took Damage");
 *             playerHealth.takeDamage(damage);
 */