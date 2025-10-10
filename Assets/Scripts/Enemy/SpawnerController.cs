using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnerController : MonoBehaviour
{

    public List<GameObject> spawnList;
    [Header("True = rightSide, false = leftSide")]
    [SerializeField] private bool spawnerSide; //true = spawner on the right, false = spawner on the left
    //When true, enemies should move to the left, when false enemies should move to the right

    private int spawnIndex = 0;




    IEnumerator spawnEnemy()
    {

        while (spawnList.Count > 0)
        {
            if (spawnerSide)
            {
                Instantiate(spawnList[spawnIndex], transform.position, Quaternion.Euler(0, 0, 0));
                spawnList[spawnIndex].GetComponent<BasicEnemyMovement>().setMoveDirection(true);
                spawnList.RemoveAt(spawnIndex);

            }
            else
            {
                Instantiate(spawnList[spawnIndex], transform.position, Quaternion.Euler(0, 0, 0));
                spawnList[spawnIndex].GetComponent<BasicEnemyMovement>().setMoveDirection(false);
                spawnList.RemoveAt(spawnIndex);

            }

            yield return new WaitForSeconds(0.7f);
        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startSpawning()
    {
        StartCoroutine(spawnEnemy());
    }

    
}
