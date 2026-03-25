using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArray : MonoBehaviour
{
    [Header("use actual game object health giver dont use the one in the prefabs folder")]
    public List<GameObject> enemyList;
    [SerializeField]private List<GameObject> spawnLocations;

    [Header("Dont touch this")]
    public List<GameObject> spawnedList;

    private void Start() {
        for(int i = 0; i < 15; i++) {
            spawnedList.Add(GameObject.Find("ToyPrison"));
        }
        spawnedList.Clear();
        SpawnEnemy();
    }

    public void KillAllEnemies() {
        for(int i = spawnedList.Count - 1; i >= 0; i--) {
            Destroy(spawnedList[i]);
        }
        spawnedList.Clear();
    }
    


    public void SpawnEnemy() {
        for(int i = 0; i < enemyList.Count && i < spawnLocations.Count; i++) {
            spawnedList.Add(Instantiate(enemyList[i], spawnLocations[i].transform.position, Quaternion.identity));
            
        }
    }
}

