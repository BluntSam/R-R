using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class healthGiver : MonoBehaviour
{
    public Health playerHealth;
    public int hpgain = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.GainHealth(hpgain);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
