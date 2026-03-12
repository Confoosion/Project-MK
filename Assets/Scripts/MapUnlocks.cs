using UnityEngine;
using UnityEngine.SceneManagement;

public class MapUnlocks : MonoBehaviour
{
    [SerializeField] private int currencyGained;
    [SerializeField] private CharacterSetSO[] characterUnlocks;

    void Awake()
    {
        if(characterUnlocks != null)
        {
            foreach(CharacterSetSO character in characterUnlocks)
            {
                Debug.Log("Unlocking " + character.name);
                ShopSaveSystem.UnlockCharacter(character.name);
            }
        }

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
