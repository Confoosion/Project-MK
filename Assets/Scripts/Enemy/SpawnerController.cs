using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum SpawnDirection { Right, Left , Random }
public class SpawnerController : MonoBehaviour
{
    public List<GameObject> spawnList;
    [SerializeField] private SpawnDirection spawnDirection; // Which way should the objects spawn

    // [SerializeField] private SpawnDirection middleSpawner; //When true, enemies should move to the left, when false enemies should move to the right

    // private int spawnIndex = 0;

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (spawnList.Count > 0)
        {
            if(spawnDirection == SpawnDirection.Random)
            {
                SpawnEnemiesIntoWorld();
            }
            else
            {
                SpawnEnemiesIntoWorld(spawnDirection == SpawnDirection.Left ? -1 : 1);                
            }
            
            yield return new WaitForSeconds(0.7f);
        }
    }

    private void SpawnEnemiesIntoWorld(int dir = 0)
    {
        if(dir == 0) // Doesn't have a direction, so randomly picks for you
            dir = Random.value < 0.5f ? -1 : 1;

        GameObject enemy = Instantiate(spawnList[0], transform.position, Quaternion.identity);
        // Instantiate(spawnList[0], transform.position);

        if (enemy.GetComponent<BasicEnemyMovement>())//make sure we get this information,....
            enemy.GetComponent<BasicEnemyMovement>().SetMoveDirection(dir); //set direction of enemy

        //SpawnerManager.Singleton.allEnemiesInWorld.Add(spawnList[spawnIndex]); //add to big enemy list

        spawnList.RemoveAt(0);
    }
}
