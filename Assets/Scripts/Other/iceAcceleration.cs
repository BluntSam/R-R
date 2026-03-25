using UnityEngine;

public class iceAcceleration : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Hopps")
        {
            //Debug.Log("Ice Ice Baby");
            Rigidbody2D rBody = collision.gameObject.GetComponent<Rigidbody2D>();
            rBody.AddForce(collision.GetContact(0).normal * 15, ForceMode2D.Impulse);
            //Debug.Log(collision.GetContact(0).normal * 15);
        }
    }
}
