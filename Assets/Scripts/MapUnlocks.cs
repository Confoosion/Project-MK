using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUnlocks : MonoBehaviour
{
    [SerializeField] private int currencyGained;
    [SerializeField] private CharacterSetSO[] characterUnlocks;
    [SerializeField] private CharacterSetSO forcedPlayerCharacter;
    // [SerializeField] private int numMapsVisited;

    void Awake()
    {
        bool justUnlocked = false;

        if(characterUnlocks != null)
        {
            foreach(CharacterSetSO character in characterUnlocks)
            {
                // Check if it was already unlocked
                if(ShopSaveSystem.IsCharacterUnlocked(character.name))
                {
                    Debug.Log("Character already unlocked");
                    continue;
                }

                // Unlocking
                Debug.Log("Unlocking " + character.name);
                ShopSaveSystem.UnlockCharacter(character.name);
                justUnlocked = true;

                // Adding to List
                CharacterManager.Singleton.AddCharacterToList(character.GetCurrentUpgrade().character);
            }
        }

        // Only becomes forcedPlayerCharacter if it was just unlocked
        if(forcedPlayerCharacter != null && justUnlocked)
        {
            CharacterManager.Singleton.BecomeNewCharacter(forcedPlayerCharacter.GetCurrentUpgrade().character);
        }

        int numMapsVisited = GameManager.Singleton.GetNumberOfMapsVisited();
        currencyGained += (numMapsVisited == 1) ? 0 : numMapsVisited;
        ShopSaveSystem.AddCurrency(currencyGained);
    }

    // void Start()
    // {
    //     Debug.Log("START LOADED");
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("SCENE LOADED");
    //     if(characterUnlocks != null)
    //     {
    //         foreach(CharacterSetSO character in characterUnlocks)
    //         {
    //             ShopSaveSystem.UnlockCharacter(character.name);
    //         }
    //     }

    //     ShopSaveSystem.AddCurrency(currencyGained);
    // }

    // void Start()
    // {
    //     if(characterUnlocks != null)
    //     {
    //         foreach(CharacterSetSO character in characterUnlocks)
    //         {
    //             ShopSaveSystem.UnlockCharacter(character.name);
    //         }
    //     }

    //     ShopSaveSystem.AddCurrency(currencyGained);
    // }
}
