using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // should take care of everything that happens throughout the level. By level i mean a single map. Should 
    //handle muffin count, difficulty. Depending on muffin count, it should tell game manager to swtich maps. 
    public static LevelManager Singleton { get; private set; }

    private int muffinCount;

    public Slider muffinProgressBar;

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
        updateProgressBar();

        MuffinSpawner.Singleton.SpawnMuffin();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addMuffin()
    {
        muffinCount++;
        updateProgressBar();
        GameManager.Singleton.addMuffinCount();

        if (GameManager.Singleton.muffinsNeededToMoveOn == muffinCount)
        {

            GameManager.Singleton.NextLevel();

        }
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

    private void updateProgressBar()
    {
        float percentageComplete = (float)muffinCount / (float)GameManager.Singleton.muffinsNeededToMoveOn;
        muffinProgressBar.value = percentageComplete;
    }
}
