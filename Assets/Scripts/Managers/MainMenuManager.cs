using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{

    [Header("Main Menu")]
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject perkMapCanvas;


    [Header("Perks")]
    [SerializeField] private PerkSelecter perkSelecter;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);


    }

    public void menuPlayButton()
    {
        mainMenuCanvas.SetActive(false);
        perkMapCanvas.SetActive(true);
    }

    public void menuBackButton()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);
    }

    public void menuStartButton()
    {
        // // Update Character List
        // CharacterManager.Singleton.UpdateCharacterList();

        // // Equip selected Perk
        if(perkSelecter.GetSelectedPerk() != null)
            PerksManager.Singleton.EquipPerk(perkSelecter.GetSelectedPerk());
        else
            PerksManager.Singleton.UnequipPerk();

        GameManager.Singleton.RestartGame();

        // // Load starting map
        // SceneManager.LoadScene("StarterMap");
    }

    public void GoToShopScene()
    {
        SceneManager.LoadScene("ShopTest");
    }

}