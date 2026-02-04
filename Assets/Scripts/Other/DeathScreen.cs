
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathScreen : MonoBehaviour
{

    public bool isDead;
    [Header("Death Screen Game Object")]
    [SerializeField] GameObject deathScreen;

    [SerializeField] PlayerInputManager playerInputManager;

    [Header("Enemy Manager Game Object")]
    [SerializeField] EnemyArray enemyArray; //Enemy Manager Game Object


    Coroutine respawnRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        deathScreen.SetActive(false);
    }

    IEnumerator Respawn() {
        enemyArray.KillAllEnemies();
        Debug.Log("IEnumerator worked");
        yield return new WaitForSeconds(2f);

        enemyArray.SpawnEnemy();
        Debug.Log("respawned enemies");
        isDead = false;
        respawnRoutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) {
            if(!deathScreen.activeSelf) {
                deathScreen.SetActive(true);
                gameObject.GetComponent<PlayerInput>().enabled = false;
                if(respawnRoutine == null) {
                    respawnRoutine = StartCoroutine(Respawn());
                }
                playerInputManager.moveInput = Vector2.zero;
            }
            
        } else {
            deathScreen.SetActive(false);
            gameObject.GetComponent<PlayerInput>().enabled = true;
        }
    }
}
