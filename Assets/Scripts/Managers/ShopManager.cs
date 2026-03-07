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
    

    void Start()
    {
        int loadedCurrency;
        ShopSaveSystem.Load(characterSets, perkMachine, out loadedCurrency);
        characterCurrency = loadedCurrency;

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
}
