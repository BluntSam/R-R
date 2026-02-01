using UnityEngine;
using System.Collections;

public class attackTriggerEnemy : MonoBehaviour
{
    //Components
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject Hopps;
    
    //Variables
    private const string PLAYER_TAG = "Player";
    private Health playerhealth;
    [SerializeField] private int damage;
    public bool isHitting = false;
    private bool canHitCooldown = false;
    [SerializeField] private float xDistance;

    private void Awake()
    {
        //Initialize
        enemy = this.transform.parent.gameObject;
        Hopps = GameObject.FindWithTag(PLAYER_TAG);
        playerhealth = Hopps.GetComponent<Health>();
        damage = enemy.GetComponent<Enemy>().AttackPower;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Flipping();
    }

    private void Flipping()
    {
        //flip trigger location based on direction
        if (enemy.GetComponent<Enemy>().moveVector.x < 0)
        {
            transform.position = new Vector2(enemy.transform.position.x - xDistance, enemy.transform.position.y);
        }
        if (enemy.GetComponent<Enemy>().moveVector.x > 0)
        {
            transform.position = new Vector2(enemy.transform.position.x + xDistance, enemy.transform.position.y);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isHitting)
        {
            if (collision.gameObject.tag == PLAYER_TAG && !canHitCooldown)
            {
                if (playerhealth == null)
                {
                    playerhealth = collision.gameObject.GetComponent<Health>();
                }

                Debug.Log("Player was Hit");
                playerhealth.TakeDamage(damage);

                isHitting = false;

                StartCoroutine(attackCooldown());
            }
        }
    }

    private IEnumerator attackCooldown()
    {
        canHitCooldown = true;
        yield return new WaitForSeconds(1f);
        canHitCooldown = false;
    }
}
