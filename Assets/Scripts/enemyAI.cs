using UnityEngine;

public class enemyAI : MonoBehaviour
{

    public GameObject Hopps;
    public float speed;

    private float distance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, Hopps.transform.position);
        Vector2 direction = Hopps.transform.position - transform.position;

        transform.position = Vector2.MoveTowards(this.transform.position, Hopps.transform.position, speed * Time.deltaTime);
    }
}
