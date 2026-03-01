using UnityEngine;
using UnityEngine.SceneManagement;

public class PerksManager : MonoBehaviour
{
    public static PerksManager Singleton { get; private set; }

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private PlayerAttack playerAttack;

    private CharacterSO character;

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

        //if (playerAttack.GetCharacter() != null)
        //{
        //    character = playerAttack.GetCharacter();
        //}
    }

    public void AddJumps(int num)
    {
        playerControl.extraJumps = num;
    }

    public void IncreaseSpeed(float num)
    {
        playerControl.MAXSPEED += num;
    }

    public void IncreaseDamage(float dmgMultiplier)
    {
        if (character)
        {
            character.attackPower *= dmgMultiplier;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {
            playerAttack = GameManager.Singleton.playerObject.GetComponent<PlayerAttack>();
            playerControl = GameManager.Singleton.playerObject.GetComponent<PlayerControl>();
        }
    }
}
