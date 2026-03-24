using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Singleton;

    void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            // DontDestroyOnLoad(this.gameObject);
        }
    }

    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI currencyText; 
    [SerializeField] private int characterCurrency;

    [Header("Characters")]
    [SerializeField] private GameObject characterShopItemPrefab;
    [SerializeField] private Transform characterShopParent;
    [SerializeField] private CharacterSetSO[] characterSets;

    [Header("Perks")]
    [SerializeField] private PerkMachineSO perkMachine;
    [SerializeField] private GachaAnimation gachaAnimation;
    [SerializeField] private PerkMachineLever perkMachineLever;
    [SerializeField] private AvailablePerksUI availablePerksUI;

    UnityEvent perkMachineEvent = new UnityEvent();
    private bool hasListener = false;
    [SerializeField] private List<PerkSO> playerPerks;

    [Space]

    [Header("Shop UI")]
    [SerializeField] private GameObject[] shopPages;
    private int currentPageIndex;
    [SerializeField] private TextMeshProUGUI switchPageText;
    [SerializeField] private TextMeshProUGUI perkMachineTitleText;
    [SerializeField] private TextMeshProUGUI perkMachineTierText;
    [SerializeField] private TextMeshProUGUI perkMachinePriceText;
    [SerializeField] private GameObject perkReceivedPopup;
    [SerializeField] private Image perkReceivedImage;
    [SerializeField] private TextMeshProUGUI perkReceivedName;
    // [SerializeField] private GameObject upgradePerkMachineButton;

    [Space]

    [Header("UI SFX")]
    [SerializeField] private AudioClip UI_ButtonSFX;
    [SerializeField] private AudioClip perkMachineUpgradeSFX;
    [SerializeField] private AudioClip buyErrorSFX;

    void Start()
    {
        int loadedCurrency;
        ShopSaveSystem.Load(characterSets, out loadedCurrency);
        characterCurrency = loadedCurrency;

        SwitchPage(0);

        UpdateCurrencyDisplay();
        PopulateCharacterShop();

        SetupPerkMachineUI();
        // UpdatePerkMachineUI();
    }

    // ========== CHARACTERS ==========
    private void PopulateCharacterShop()
    {
        foreach(CharacterSetSO set in characterSets)
        {
            GameObject item = Instantiate(characterShopItemPrefab, characterShopParent);
            item.GetComponent<CharacterShopUI>().Initialize(set);
        }
    }

    // ========== PERKS ==========
    public void PullPerk()
    {
        PerkSO perk = PerkGachaManager.Singleton.RollGacha();
        if(perk != null)
        {
            gachaAnimation.StartSpin(perk, perkMachine.GetAvailablePerks());

            if(!playerPerks.Contains(perk))
            {
                playerPerks.Add(perk);
            }
        }
        else
        {
            perkMachineLever.UpdateLever(false);
            SoundManager.Singleton.PlayUIAudio(buyErrorSFX);
        }
    }

    public void UpgradePerkMachine()
    {
        if(perkMachine.UpgradePerkMachine())
        {
            SoundManager.Singleton.PlayUIAudio(perkMachineUpgradeSFX);
            UpdateUpgradePerkMachineUI();
            availablePerksUI.SetAvailablePerks(perkMachine.GetAvailablePerks());
        }
        else
            SoundManager.Singleton.PlayUIAudio(buyErrorSFX);
        perkMachineLever.UpdateLever(false);
    }

    public void AddEvent_PullPerk()
    {
        if(hasListener)
        {
            perkMachineEvent.RemoveAllListeners();
        }
    
        perkMachineEvent.AddListener(PullPerk);
        hasListener = true;

        // Update UI
        perkMachineTitleText.SetText("Pull Perk");
        perkMachinePriceText.SetText(PerkGachaManager.Singleton.GetGachaPrice().ToString() + "c");
    }

    public void AddEvent_UpgradePerkMachine()
    {
        if(hasListener)
        {
            perkMachineEvent.RemoveAllListeners();
            hasListener = true;
        }

        perkMachineEvent.AddListener(UpgradePerkMachine);

        // Update UI
        UpdateUpgradePerkMachineUI();
    }

    public void LeverPulled()
    {
        if(hasListener)
        {
            perkMachineEvent.Invoke();
        }
        else
        {
            perkMachineLever.UpdateLever(false);
            SoundManager.Singleton.PlayUIAudio(buyErrorSFX);
        }
    }

    // ========== CURRENCY ========== 
    public int GetCharacterCurrency()
    {
        return(characterCurrency);
    }

    public void AddCharacterCurrency(int amount)
    {
        characterCurrency += amount;
        ShopSaveSystem.SetCurrency(characterCurrency);
        UpdateCurrencyDisplay();
        SaveShop();
    }

    private void UpdateCurrencyDisplay()
    {
        currencyText.SetText(characterCurrency.ToString());
    }

    // ========== SAVING ==========
    private void OnApplicationQuit()
    {
        SaveShop();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if(pauseStatus)
        {
            SaveShop();
        }
    }

    public void SaveShop()
    {
        ShopSaveSystem.Save(characterSets, characterCurrency);
    }

    public void ResetShop()
    {
        ShopSaveSystem.DeleteSaveData();

        // Reset Characters
        foreach(var charSet in characterSets)
        {
            CharacterSetData data = ShopSaveSystem.GetCharacterData(charSet.name);
            if(data != null)
            {
                data.currentLevel = 1;
                data.isUnlocked = false;
            }
        }

        if(characterSets.Length > 0)
        {
            ShopSaveSystem.UnlockCharacter(characterSets[0].name);
            ShopSaveSystem.UnlockCharacter(characterSets[1].name);
            ShopSaveSystem.UnlockCharacter(characterSets[2].name);
        }

        foreach(Transform child in characterShopParent)
        {
            Destroy(child.gameObject);
        }
        PopulateCharacterShop();

        // Reset Perks
        PerkMachineData perkMachineData = ShopSaveSystem.GetPerkMachineData();
        if(perkMachineData != null)
        {
            perkMachineData.currentTier = 1;
            perkMachineData.ownedPerks.Clear();
            perkMachineData.pityCounter = 0;
        }

        SetupPerkMachineUI();

        // Reset Currency
        characterCurrency = 0;
        ShopSaveSystem.SetCurrency(0);
    }

    // ========== SHOP UI ==========
    public void SwitchPage(int pageDirection = 0)
    {
        // Initialize page
        if(pageDirection == 0)
        {
            foreach(GameObject page in shopPages)
            {
                page.SetActive(false);
            }

            shopPages[0].SetActive(true);
            currentPageIndex = 0;
        }
        // Swap page
        else 
        {
            shopPages[currentPageIndex].SetActive(false);

            currentPageIndex += pageDirection;
            if(currentPageIndex < 0)
                currentPageIndex = shopPages.Length - 1;
            else if(currentPageIndex > shopPages.Length - 1)
                currentPageIndex = 0; 

            shopPages[currentPageIndex].SetActive(true);
        }

        int underscore = shopPages[currentPageIndex].name.IndexOf("_");
        string pageText = shopPages[currentPageIndex].name.Substring(0, underscore);
        switchPageText.SetText(pageText + " Page");

        SetupPerkMachineUI();
    }

    private void SetupPerkMachineUI()
    {
        // Hide Perk Popup
        HidePerkReceivedUI();

        // Remove any listeners in the UnityEvent
        perkMachineEvent.RemoveAllListeners();
        hasListener = false;

        // Set up Title text
        perkMachineTitleText.SetText("Perk Machine");

        // Set up Tier text
        int currTier = perkMachine.GetCurrentTier();
        perkMachineTierText.SetText("Tier " + currTier.ToString());

        // Set up Price text
        perkMachinePriceText.SetText("");

        // Set up Available Perks list
        availablePerksUI.SetAvailablePerks(perkMachine.GetAvailablePerks());
    } 

    private void UpdateUpgradePerkMachineUI()
    {
        int currTier = perkMachine.GetCurrentTier();
        perkMachineTierText.SetText("Tier " + currTier.ToString());
        
        PerkSetSO nextTierSettings;
    
        if(currTier + 1 <= perkMachine.tiers.Length)
        {
            nextTierSettings = perkMachine.tiers[currTier];
            
            perkMachineTitleText.SetText("Upgrade Machine");
            perkMachinePriceText.SetText(nextTierSettings.upgradePrice.ToString() + "c");
        }
        else
        {
            perkMachineTitleText.SetText("MAX TIER REACHED");
            perkMachinePriceText.SetText("");
        }
    }

    public void ShowPerkReceivedUI(PerkSO perk)
    {
        perkReceivedPopup.SetActive(true);

        if(perk != null)
        {
            perkReceivedImage.sprite = perk.icon;
            perkReceivedName.SetText(perk.perkName);
        }
    }

    public void HidePerkReceivedUI()
    {
        perkReceivedPopup.SetActive(false);
        perkMachineLever.UpdateLever(false);
    }

    public void GoToMainMenuScene()
    {
        SaveShop();
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayButtonPress_SFX()
    {
        SoundManager.Singleton.PlayUIAudio(UI_ButtonSFX);
    }
}
