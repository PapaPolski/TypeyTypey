using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public float coolDownTime;

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

       // yield return new WaitUntil(() => { return GameManager.Instance.CurrentGameState == GameState.Playing; });
        while(GameManager.Instance.CurrentGameState != GameState.Playing) { yield return null; }
        
        yield return new WaitForSeconds(interval);
        Vector3 enemySpawnLocation = new Vector3(Random.Range(-8f, 1.5f), 5.6f, 0f);
        if(GameManager.Instance.CurrentGameState == GameState.Playing)
            Instantiate(SelectEnemy(enemies), enemySpawnLocation, Quaternion.identity);
        StartCoroutine(SpawnEnemy(coolDownTime));
    }
}
