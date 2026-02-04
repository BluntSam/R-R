using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]private Health health;

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            health.spawnLocation = transform.position;
            Debug.Log("set spawnpoint");
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }


}


