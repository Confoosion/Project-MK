using UnityEngine;

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
}
