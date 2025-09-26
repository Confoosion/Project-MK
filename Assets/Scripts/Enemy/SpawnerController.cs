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

    private int spawnIndex;




    IEnumerator spawnEnemy()
    {
        spawnIndex = spawnList.Count - 1;
        while (spawnList.Count > 0)
        {
            if (spawnerSide)
            {
                Instantiate(spawnList[spawnIndex], transform.position, Quaternion.Euler(0, 0, 0));
                spawnList[spawnIndex].GetComponent<EnemyController>().setMoveDirection(true);
                spawnList.RemoveAt(spawnIndex);
                spawnIndex--;
            } else
            {
                Instantiate(spawnList[spawnIndex], transform.position, Quaternion.Euler(0, 0, 0));
                spawnList[spawnIndex].GetComponent<EnemyController>().setMoveDirection(false);
                spawnList.RemoveAt(spawnIndex);
                spawnIndex--;
            }

            yield return new WaitForSeconds(0.7f);
        }
        
    }
    
    void Start()
    {
        StartCoroutine(spawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
