using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [Header("Player")]
    [SerializeField] public GameObject playerObject;
    public GameObject spawnedPlayer {get; private set;}

    [SerializeField] private GameObject endScreen;
    public int muffinsNeededToMoveOn = 5;

    // public List<CharacterSO> characterListGM;

    // public static int mapNumber;
    // public static PerkSO perk;
    private EndScreenScript endScreenScript;

    // [Header("Character SO")]
    // [SerializeField] public CharacterSO BlackHoleCharacter; //1
    // [SerializeField] public CharacterSO BoomerangCharacter; //2
    // [SerializeField] public CharacterSO BowlingBallCharacter; //3
    // [SerializeField] public CharacterSO BurstProjectileCharacter; //4
    // [SerializeField] public CharacterSO ChargerCharacter; //5
    // [SerializeField] public CharacterSO FlamethrowerCharacter; //6
    // [SerializeField] public CharacterSO FreezeRayCharacter; //7
    // [SerializeField] public CharacterSO GrenadeCharacter; //8
    // [SerializeField] public CharacterSO GroundPoundCharacter; //9
    // [SerializeField] public CharacterSO LandMineCharacter; //10
    // [SerializeField] public CharacterSO LaserGunCharacter; //11
    // [SerializeField] public CharacterSO NukeCharacter; //12
    // [SerializeField] public CharacterSO ShotgunCharacter; //13

    private List<String> maps = new List<string> { "BoomerangMap", "BombMap", "LandMineMap" };

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

        endScreenScript = gameObject.GetComponent<EndScreenScript>();
    }

    void Start()
    {
        int tempCurrency = 0;
        ShopSaveSystem.Load(CharacterManager.Singleton.GetFullCharacterList(), out tempCurrency);
    }

    public void NextLevel()
    {
        int getNewMap = GetRandomNumber(0, maps.Count);
        string mapString = maps[getNewMap];
        
        muffinsNeededToMoveOn += muffinsNeededToMoveOn;

        switch (mapString)
        {
            case "BoomerangMap":
                SceneManager.LoadScene("BoomerangMap");
                // characterListGM.Add(BoomerangCharacter);
                maps.Remove("BoomerangMap");
                break;

            case "BombMap":
                SceneManager.LoadScene("BombMap");
                // characterListGM.Add(GrenadeCharacter);
                maps.Remove("BombMap");
                break;

            case "LandMineMap":
                SceneManager.LoadScene("LandMineMap");
                // characterListGM.Add(LandMineCharacter);
                maps.Remove("LandMineMap");
                break;

        }

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

    }
    public void disableEndScreen()
    {
        endScreen.SetActive(false);
        endScreenScript.updateText();
    }


    private int GetRandomNumber(int min, int max) //max exclusive
    {
        return UnityEngine.Random.Range(min, max);
    }



}
