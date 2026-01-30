using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Singleton { get; private set; }

    [SerializeField] private GameObject endScreen;
    [SerializeField] private List<int> normalMaps;
    [SerializeField] private List<int> fantasyMaps;
    [SerializeField] private List<int> allMaps;
    public static int mapNumber;
    public static PerkSO perk;
    private EndScreenScript endScreenScript;

    private int muffinSum = 0;
    private int basicEnemiesKilled = 0;
    private int angryBasicEnemiesKilled = 0;
    private int angryHeavyEnemiesKilled = 0;
    private int heavyEnemiesKilled = 0;

    void Awake()
    {
        if (Singleton != null && Singleton != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Singleton = this;
        DontDestroyOnLoad(this.gameObject);

        endScreenScript = gameObject.GetComponent<EndScreenScript>();
    }

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






}
