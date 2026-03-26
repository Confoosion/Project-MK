using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [Header("Player")]
    private bool isAlive = true;
    [SerializeField] public GameObject playerObject;
    public GameObject spawnedPlayer {get; private set;}

    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private int START_MUFFINS_NEEDED = 5;

    [Space]
    [SerializeField] private int muffinsNeededToMoveOn = 5;

    private EndScreenScript endScreenScript;

    private int mapCount;
    private List<int> visitedMapIndices = new List<int>();

    // variables for the whole game. End of game stats
    private int muffinSum = 0;
    private int basicEnemiesKilled = 0;
    private int angryBasicEnemiesKilled = 0;
    private int angryHeavyEnemiesKilled = 0;
    private int heavyEnemiesKilled = 0;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        mapCount = SceneManager.sceneCountInBuildSettings - 3; // subtract 3 removes MainMenu, Shop, and RuinedCity 
        endScreenScript = gameObject.GetComponent<EndScreenScript>();
    }

    void Start()
    {
        int tempCurrency = 0;
        ShopSaveSystem.Load(CharacterManager.Singleton.GetFullCharacterList(), out tempCurrency);
    }

    public void RestartGame()
    {
        CharacterManager.Singleton.UpdateCharacterList();

        disableEndScreen();
        disableDeathScreen();

        resetEnemyKills();
        resetMuffinCount();
        resetMapPool();

        muffinsNeededToMoveOn = START_MUFFINS_NEEDED;

        SceneManager.LoadScene("RuinedCityMap");

        if(PlayerControl.Singleton)
        {
            PlayerControl.Singleton.movementLocked = false;
            isAlive = true;
        }
    }

    public void NextLevel()
    {
        if (visitedMapIndices.Count == mapCount)
        {
            PlayerControl.Singleton.movementLocked = true;
            enableEndScreen();
            SpawnerManager.Singleton.stopGame = true;

            return;
        }

        int newMapIndex = GetRandomNumber(0, mapCount) + 3; // skip MainMenu, Shop, and Ruined City
        
        while (visitedMapIndices.Contains(newMapIndex)) // find map not visited yet
        {
            newMapIndex = GetRandomNumber(0, mapCount) + 3;
        }

        visitedMapIndices.Add(newMapIndex);

        SceneManager.LoadScene(newMapIndex);

        muffinsNeededToMoveOn += muffinsNeededToMoveOn;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu" && scene.name != "ShopTest")
        {
            if (!GameObject.FindGameObjectWithTag("Player"))
            {
                spawnedPlayer = Instantiate(playerObject);
                CharacterManager.Singleton.characterTransform = spawnedPlayer.transform;
                CharacterManager.Singleton.characterModel = spawnedPlayer.GetComponentInChildren<SpriteRenderer>();
            }

            if (spawnedPlayer != null)
            {
                Transform spawnPoint = GetRandomSpawnPoint();
                if (spawnPoint != null)
                {
                    spawnedPlayer.transform.position = spawnPoint.position;
                }
            }
        }
    }

    private Transform GetRandomSpawnPoint()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        return spawnPoints.Length > 0 ? spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)].transform : null;
    }

    private void resetMapPool()
    {
        visitedMapIndices.Clear();
    }

    public int GetCurrentMuffinsNeeded()
    {
        return(muffinsNeededToMoveOn);
    }

    //end screen stats stuff

    public int getMuffinSum()
    {
        return muffinSum;
    }

    public int getBasicEnemiesKilled()
    {
        return basicEnemiesKilled;
    }

    public int getHeavyEnemiesKilled()
    {
        return heavyEnemiesKilled;
    }

    public int getAngryBasicEnemiesKilled()
    {
        return angryBasicEnemiesKilled;
    }

    public int getAngryHeavyEnemiesKilled()
    {
        return angryHeavyEnemiesKilled;
    }

    public void addMuffinCount()
    {
        muffinSum++;
    }

    public void resetMuffinCount()
    {
        muffinSum = 0;
    }

    public void addBasicEnemyKill()
    {
        basicEnemiesKilled++;
    }

    public void addHeavyEnemyKill()
    {
        heavyEnemiesKilled++;
    }

    public void addAngryBasicEnemyKill()
    {
        angryBasicEnemiesKilled++;
    }

    public void addAngryHeavyEnemyKill()
    {
        angryHeavyEnemiesKilled++;
    }

    public void resetEnemyKills()
    {
        basicEnemiesKilled = 0;
        heavyEnemiesKilled = 0;
        angryBasicEnemiesKilled = 0;
        angryHeavyEnemiesKilled = 0;
    }

    public void enableEndScreen()
    {
        endScreen.SetActive(true);
        endScreenScript.updateText();
        endScreenScript.updateText2();
    }
    public void disableEndScreen()
    {
        endScreen.SetActive(false);
        endScreenScript.updateText();
        endScreenScript.updateText2();
    }

    public void enableDeathScreen()
    {
        isAlive = false;
        deathScreen.SetActive(true);
        endScreenScript.updateText();
        endScreenScript.updateText2();
    }

    public void disableDeathScreen()
    {
        deathScreen.SetActive(false);
        endScreenScript.updateText();
        endScreenScript.updateText2();
    }

    private int GetRandomNumber(int min, int max) //max exclusive
    {
        return UnityEngine.Random.Range(min, max);
    }

    public bool IsPlayerAlive()
    {
        return(isAlive);
    }

}
