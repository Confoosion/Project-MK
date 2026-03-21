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

    [Header("SFX")]
    [SerializeField] private AudioClip UI_SFX;

    void Start()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);
    }

    public void menuPlayButton()
    {
        mainMenuCanvas.SetActive(false);
        perkMapCanvas.SetActive(true);
        PlaySFX();
    }

    public void menuBackButton()
    {
        mainMenuCanvas.SetActive(true);
        perkMapCanvas.SetActive(false);
        PlaySFX();
    }

    public void menuStartButton()
    {
        // // Update Character List
        // CharacterManager.Singleton.UpdateCharacterList();

        PlaySFX();

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
        PlaySFX();
        
        SceneManager.LoadScene("ShopTest");
    }

    private void PlaySFX()
    {
        SoundManager.Singleton.PlayUIAudio(UI_SFX);
    }

}