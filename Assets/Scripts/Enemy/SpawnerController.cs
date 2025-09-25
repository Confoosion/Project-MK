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




    IEnumerator spawnEnemy()
    {
        while(spawnList.Count > 0)
        {
            if (spawnerSide)
            {

            }
        }
        yield return null;
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
