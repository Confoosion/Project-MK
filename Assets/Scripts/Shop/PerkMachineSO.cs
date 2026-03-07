using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "PerkMachine", menuName = "Shop/Perk Machine")]
public class PerkMachineSO : ScriptableObject
{
    [Header("Gacha Tiers")]
    public PerkSetSO[] tiers = new PerkSetSO[3];

    [Header("Rarity Weights")]
    [Range(0, 100)] public float commonWeight = 60f;
    [Range(0, 100)] public float rareWeight = 30f;
    // [Range(0, 100)] public float epicWeight = 9f;        // Idk if we're doing these rarities yet
    // [Range(0, 100)] public float legendaryWeight = 1f;   // Not enough perks to know yet

    public int GetCurrentTier()
    {
        PerkMachineData data = ShopSaveSystem.GetPerkMachineData();
        return data != null ? data.currentTier : 1;
    }

    public PerkSO[] GetAvailablePerks()
    {
        int currentTier = GetCurrentTier();
        List<PerkSO> availablePerks = new List<PerkSO>();

        for(int i = 0; i < currentTier && i < tiers.Length; i++)
        {
            availablePerks.AddRange(tiers[i].perksUnlocked);
        }

        return(availablePerks.ToArray());
    }

    public PerkSetSO GetCurrentTierSettings()
    {
        int tier = GetCurrentTier();
        return(tiers[Mathf.Clamp(tier - 1, 0, tiers.Length - 1)]);
    }

    public void UpgradePerkMachine()
    {
        int currentTier = GetCurrentTier();

        if(currentTier >= tiers.Length)
        {
            Debug.Log("Already max tier Perk Machine!");
            return;
        }

        int upgradePrice = tiers[currentTier].upgradePrice;
        if(ShopManager.Singleton.GetCharacterCurrency() < upgradePrice)
        {
            Debug.Log("Too broke to upgrade Perk Machine!");
            return;
        }

        ShopManager.Singleton.AddCharacterCurrency(-upgradePrice);

        ShopSaveSystem.UpgradePerkMachineTier(currentTier + 1);

        ShopManager.Singleton.SaveShop();

        Debug.Log("Gacha upgraded to tier " + currentTier + 1 + "\n" + tiers[currentTier].perksUnlocked.Length + " new perks unlocked!");
    }
}
