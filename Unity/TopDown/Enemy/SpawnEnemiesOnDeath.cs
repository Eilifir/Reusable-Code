using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemiesOnDeath : MonoBehaviour
{
    [SerializeField] GameObject enemyToSpawn;
    EnemyHandler mainScript;
    private void Start()
    {
        mainScript = GetComponent<EnemyHandler>();
    }
    private void OnDestroy()
    {
        GameObject enemySpawned = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        EnemyHandler enemySpawned = enemySpawned.GetComponent<EnemyHandler>();
        slimeOneScript.currentWaypoint =  mainScript.currentWaypoint;
    }
}
