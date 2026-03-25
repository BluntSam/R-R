using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class healthGiver : MonoBehaviour
{
    public Health playerHealth;
    public int hpgain = 1;

   
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.GainHealth(hpgain);
            StartCoroutine(Cooldown());
        }
    }

    //deletes itself
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
