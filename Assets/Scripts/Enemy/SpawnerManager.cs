using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SpawnerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SpawnerManager Singleton { get; private set; }

[Header("Enemies")]
    [SerializeField] private GameObject normalEnemy;
    [SerializeField] private GameObject heavyEnemy;
    [SerializeField] private GameObject flyingEnemy;


[Header("OtherVariables")]
    

    [SerializeField] public List<GameObject> spawnersInWorld;
    [SerializeField] private int waveNumber = 0;
    private int spawnerIndex = 0;

    private List<GameObject> specificSpawnList;

    [SerializeField]private bool stopGame = false;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    void Start()
    {
        StartCoroutine(startGame());
        
    }

    IEnumerator startGame()
    {
        while (!stopGame)
        {
            populateSpawners(waveNumber);
            yield return new WaitForSeconds(5.0f);
        }
        
    }

    private void populateSpawners(int wave)
    {
        specificSpawnList = spawnersInWorld[spawnerIndex].GetComponent<SpawnerController>().spawnList;
        if (wave < 5)
        {
            //add random number (2-4) of enemies (normal) into spawner
            int randNum = GetRandomNumber(2, 4);
            for (int i = 0; i < randNum; i++)
            {
                specificSpawnList.Add(normalEnemy);
            }
            //spawn normal enemies
        }

        else if (wave >= 5 && wave < 10)
        {
            //spawn normal and heavy enemies
            //add random number (3 - 6) of enemies (normal / heavy) into spawner
            int randNum = GetRandomNumber(3, 6);
            for (int i = 0; i < randNum; i++)
            {
                float temp = Random.value;
                if (temp <= 0.5f)
                {
                    specificSpawnList.Add(normalEnemy);
                }
                else
                {
                    specificSpawnList.Add(heavyEnemy);
                }
            }
        }

        else if (wave >= 10)
        {
            //spawn normal, heavy, and flying
            //add random number (5-9) of enemies (normal / heavy / flying) into spawner
            int randNum = GetRandomNumber(3, 6);
            for (int i = 0; i < randNum; i++)
            {
                float temp = Random.value;
                if (temp <= 0.33f)
                {
                    specificSpawnList.Add(normalEnemy);
                }
                else if (temp > 0.33f && temp <= 0.66f)
                {
                    specificSpawnList.Add(heavyEnemy);
                }
                else
                {
                    specificSpawnList.Add(flyingEnemy);
                }
            }
        }

        //update information
        spawnersInWorld[spawnerIndex].GetComponent<SpawnerController>().startSpawning();
        updateSpawnerIndex();
        waveNumber++;

    }

    private void updateSpawnerIndex()
    {
        spawnerIndex++;
        if (spawnerIndex >= spawnersInWorld.Count)
        {
            spawnerIndex = 0;
        }
    }

    private int GetRandomNumber(int min, int max)
    {
        return Random.Range(min, max + 1);
    }

    
    
}
