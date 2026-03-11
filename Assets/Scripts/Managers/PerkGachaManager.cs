using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PerkGachaManager : MonoBehaviour
{
    public static PerkGachaManager Singleton;

    [SerializeField] private int GACHA_PRICE;
    [SerializeField] private int MAX_PITY;
    [SerializeField] private PerkMachineSO perkMachine;

    public System.Action<PerkSO, bool> OnPerkRolled; // (perk, isNew)

    void Awake()
    {
        if(Singleton == null)
            Singleton = this;
    }   

    public PerkSO RollGacha()
    {
        PerkSetSO currentTier = perkMachine.GetCurrentTierSettings();

        // Check if player has enough currency
        if(ShopManager.Singleton.GetCharacterCurrency() < GACHA_PRICE)
        {
            Debug.Log("Too broke to gacha!");
            return null;
        }
        ShopManager.Singleton.AddCharacterCurrency(-GACHA_PRICE);

        PerkSO[] availablePerks = perkMachine.GetAvailablePerks();

        if(availablePerks.Length == 0)
        {
            Debug.LogWarning("No perks in the machine!");
            return null;
        }

        PerkMachineData perkMachineData = ShopSaveSystem.GetPerkMachineData();

        List<string> ownedPerks = new List<string>();
        foreach(PerkData perkData in perkMachineData.ownedPerks)
        {
            if(perkData.isUnlocked)
            {
                ownedPerks.Add(perkData.perkName);
            }
        }

        perkMachineData.pityCounter++;

        PerkSO rolledPerk;
        bool isNewPerk = false;

        if(perkMachineData.pityCounter >= MAX_PITY)
        {
            rolledPerk = RollNewPerk(availablePerks, ownedPerks);
            perkMachineData.pityCounter = 0;
            isNewPerk = true;
            Debug.Log("PITY ROLL");
        }
        else
        {
            rolledPerk = RollRandomPerk(availablePerks);

            isNewPerk = !ownedPerks.Contains(rolledPerk.name);
        }

        if(isNewPerk)
        {
            ShopSaveSystem.UnlockPerk(rolledPerk.name);
            perkMachineData.pityCounter = 0;
            Debug.Log("NEW PERK: " + rolledPerk.perkName);
        }
        else
        {
            Debug.Log("Duplicate: " + rolledPerk.perkName + " Pity: " + perkMachineData.pityCounter + "/" + MAX_PITY);
        }

        ShopManager.Singleton.SaveShop();

        OnPerkRolled?.Invoke(rolledPerk, isNewPerk);

        return(rolledPerk);
    }

    private PerkSO RollNewPerk(PerkSO[] availablePerks, List<string> ownedPerks)
    {
        PerkSO[] newPerks = availablePerks.Where(p => !ownedPerks.Contains(p.name)).ToArray();

        if(newPerks.Length == 0)
        {
            Debug.Log("All perks owned already!");
            return(availablePerks[Random.Range(0, availablePerks.Length)]);
        }

        return newPerks[Random.Range(0, newPerks.Length)];
    }

    private PerkSO RollRandomPerk(PerkSO[] availablePerks)
    {
        return(availablePerks[Random.Range(0, availablePerks.Length)]);
    }

    public bool HasPerk(PerkSO perk)
    {
        // PerkMachineData data = ShopSaveSystem.GetPerkMachineData();
        // return(data.ownedPerks.Contains(perk.name));
        return(ShopSaveSystem.IsPerkUnlocked(perk.name));
    }

    public List<PerkSO> GetOwnedPerks()
    {
        List<PerkData> unlockedPerks = ShopSaveSystem.GetUnlockedPerks();

        PerkMachineData data = ShopSaveSystem.GetPerkMachineData();
        PerkSO[] allPerks = perkMachine.GetAvailablePerks();

        List<PerkSO> owned = new List<PerkSO>();
        foreach(PerkData perkData in unlockedPerks)
        {
            PerkSO perk = System.Array.Find(allPerks, p => p.name == perkData.perkName);
            if(perk != null)
            {
                owned.Add(perk);
            }
        }

        return(owned);
    }

    public PerkMachineSO GetPerkMachine()
    {
        return(perkMachine);
    }
}