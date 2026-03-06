using UnityEngine;

[CreateAssetMenu(fileName = "PerkSet", menuName = "Shop/Perk Set")]
public class PerkSetSO : ScriptableObject
{
    [Header("Tier Info")]
    public int tierLevel;
    public int upgradePrice;

    [Header("Perks in this Tier")]
    public PerkSO[] perksUnlocked;
}
