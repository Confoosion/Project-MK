using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Perk Database", menuName = "Perks/Perk Database")]
public class PerkDatabase : ScriptableObject
{
    [Header("All Perks in Game")]
    public PerkSO[] allPerks;
    
    [Space]
    public PerkMachineSO perkMachine;  // Reference to your gacha machine SO
    
    /// <summary>
    /// Find a perk by name
    /// </summary>
    public PerkSO GetPerkByName(string perkName)
    {
        return System.Array.Find(allPerks, p => p.name == perkName);
    }
    
    /// <summary>
    /// Get all unlocked perks as PerkSO objects (works in any scene!)
    /// </summary>
    public List<PerkSO> GetUnlockedPerks()
    {
        List<PerkSO> unlockedPerkObjects = new List<PerkSO>();
        List<PerkData> unlockedData = ShopSaveSystem.GetUnlockedPerks();
        
        foreach (var perkData in unlockedData)
        {
            PerkSO perk = GetPerkByName(perkData.perkName);
            if (perk != null)
            {
                unlockedPerkObjects.Add(perk);
            }
        }
        
        return unlockedPerkObjects;
    }
    
    /// <summary>
    /// Check if a specific perk is unlocked (works in any scene!)
    /// </summary>
    public bool IsPerkUnlocked(PerkSO perk)
    {
        return ShopSaveSystem.IsPerkUnlocked(perk.name);
    }
    
    /// <summary>
    /// Get available perks from gacha at current tier (works in any scene!)
    /// </summary>
    public PerkSO[] GetAvailablePerksInPerkMachine()
    {
        if (perkMachine == null)
        {
            Debug.LogWarning("No gacha machine assigned to Perk Database!");
            return new PerkSO[0];
        }
        
        return perkMachine.GetAvailablePerks();
    }
}