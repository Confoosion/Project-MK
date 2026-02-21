using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static LevelManager Singleton { get; private set; }

    private int muffinCount;

    public TMP_Text muffinCountText;

    public int difficulty = 1;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }
    void Start()
    {
        resetMuffinCount();
        resetDifficulty();

        MuffinSpawner.Singleton.SpawnMuffin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addMuffin()
    {
        muffinCount++;
        GameManager.Singleton.addMuffinCount();
        muffinCountText.text = "Muffin: " + muffinCount;
    }

    public void increaseDifficulty()
    {
        difficulty++;
    }

    public void resetDifficulty()
    {
        difficulty = 1;
    }

    public void resetMuffinCount()
    {
        muffinCount = 0;
    }
}
