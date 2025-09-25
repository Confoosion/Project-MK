using UnityEngine;
using System.Collections.Generic;

public class SpawnerManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static SpawnerManager Singleton { get; private set; }
    [SerializeField] private List<GameObject> enemyPrefabList;

    [SerializeField] private int waveNumber = 0;

    [SerializeField] public List<GameObject> spawnersInWorld;


    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    //ENEMY LIST
    public List<GameObject> getEnemyList()
    {
        return enemyPrefabList;
    }


    //WAVES
    public int getWaveNumber()
    {
        return waveNumber;
    }

    public void setWaveNumber(int newWave)
    {
        waveNumber = newWave;
    }
}
