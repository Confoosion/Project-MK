using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Singleton;

    void Awake()
    {
        if(Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    [Header("Currency")]
    [SerializeField] private TextMeshProUGUI currencyText; 
    [SerializeField] private int characterCurrency;

    [Header("Characters")]
    [SerializeField] private CharacterSetSO[] characterSets;
    [SerializeField] private GameObject characterShopItemPrefab;
    [SerializeField] private Transform characterShopParent;

    // Perks eventually
    [Header("Perks")]
    [SerializeField] private PerkMachineSO perkMachine;

    [Space]

    [Header("Shop UI")]
    [SerializeField] private GameObject[] shopPages;
    private int currentPageIndex;
    [SerializeField] private TextMeshProUGUI switchPageText;

    void Start()
    {
        int loadedCurrency;
        ShopSaveSystem.Load(characterSets, perkMachine, out loadedCurrency);
        characterCurrency = loadedCurrency;

        SwitchPage(0);

        UpdateCurrencyDisplay();
        PopulateCharacterShop();
    }

    private void PopulateCharacterShop()
    {
        foreach(CharacterSetSO set in characterSets)
        {
            GameObject item = Instantiate(characterShopItemPrefab, characterShopParent);
            item.GetComponent<CharacterShopUI>().Initialize(set);
        }
    }

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
        ShopSaveSystem.Save(characterSets, characterCurrency, perkMachine);
    }

    public void ResetShop()
    {
        ShopSaveSystem.DeleteSaveData();

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
    }

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
    }
}
