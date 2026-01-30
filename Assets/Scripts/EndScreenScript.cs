using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private TMP_Text muffinsCollectedText;
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text moreEnemyStatsText;




    void Start()
    {
        moreEnemyStatsText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void moreEnemyStatsButton()
    {
        if (moreEnemyStatsText.enabled)
        {
            moreEnemyStatsText.enabled = false;
        }
        else
        {
            moreEnemyStatsText.enabled = true;

        }

    }

    public void mainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
        GameManager.Singleton.disableEndScreen();

        GameManager.Singleton.resetEnemyKills();
        GameManager.Singleton.resetMuffinCount();
    }

    public void updateText()
    {
        muffinsCollectedText.text = "Muffins Collected: " + GameManager.Singleton.getMuffinSum();
        enemiesKilledText.text = "Enemies Killed: " + (GameManager.Singleton.getBasicEnemiesKilled() + GameManager.Singleton.getHeavyEnemiesKilled() + GameManager.Singleton.getAngryBasicEnemiesKilled() + GameManager.Singleton.getAngryHeavyEnemiesKilled());
        moreEnemyStatsText.text = "Basic Enemy Deaths: " + GameManager.Singleton.getBasicEnemiesKilled() + "\n" + "Heavy Enemy Deaths: " + GameManager.Singleton.getHeavyEnemiesKilled() + "\nAngry Basic Enemy Deaths: " + GameManager.Singleton.getAngryBasicEnemiesKilled() + "\nAngry Heavy Enemy Deaths: " + GameManager.Singleton.getAngryHeavyEnemiesKilled();
    }
}
