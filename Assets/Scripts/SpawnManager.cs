using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public float coolDownTime;
    public Transform[] spawnLocations;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy(coolDownTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject SelectEnemy(GameObject[] enemiesList)
    {
        int randomIndex;
        GameObject randomEnemy = null;
        
        while(randomEnemy == null || !randomEnemy.GetComponent<Enemy>().isEnemyUnlocked)
        {
            randomEnemy = enemiesList[(randomIndex = Random.Range(0, enemiesList.Length))];
        }

        return randomEnemy;
    }

    IEnumerator SpawnEnemy(float interval)
    {
        while(GameManager.Instance.CurrentGameState != GameState.Playing) { yield return null; }
        
        yield return new WaitForSeconds(interval);
        int r;
        Transform enemySpawnLocation = spawnLocations[(r = Random.Range(0, spawnLocations.Length))];
        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            GameObject enemyToSpawn = Instantiate(SelectEnemy(enemies), enemySpawnLocation.position, Quaternion.identity);
        }
            StartCoroutine(SpawnEnemy(coolDownTime));
    }
}
