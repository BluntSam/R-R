using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Health health;

    private void Start()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.spawnLocation = transform.position;
        }
    }


}


