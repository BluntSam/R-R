using UnityEngine;

public class enemyAI : MonoBehaviour
{

    public GameObject Hopps;
    public float speed;

    private float distance;

    void Update()
    {
        distance = Vector2.Distance(transform.position, Hopps.transform.position);
        Vector2 direction = Hopps.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime);
    }
}
