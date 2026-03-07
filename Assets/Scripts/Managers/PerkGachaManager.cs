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
        List<string> ownedPerks = perkMachineData.ownedPerks;

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
            ownedPerks.Add(rolledPerk.name);
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
        PerkMachineData data = ShopSaveSystem.GetPerkMachineData();
        return(data.ownedPerks.Contains(perk.name));
    }

    public List<PerkSO> GetOwnedPerks()
    {
        PerkMachineData data = ShopSaveSystem.GetPerkMachineData();
        PerkSO[] allPerks = perkMachine.GetAvailablePerks();

        List<PerkSO> owned = new List<PerkSO>();
        foreach(var perk in allPerks)
        {
            if(data.ownedPerks.Contains(perk.name))
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