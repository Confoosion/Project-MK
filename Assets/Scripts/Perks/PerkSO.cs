using UnityEngine;

public enum PerkType
{
    DoubleJump,
    Speed,
    Damage,
    SlowEnemies,
    FutureTeller
}

[CreateAssetMenu(menuName = "Perks/Perk")]
public class PerkSO : ScriptableObject
{
    public string perkName;
    [TextArea] public string perkDescription;
    public Sprite icon;

    public PerkType perkType;
    public float value; // jumps, speed amount, dmg multiplier, slow %, etc.
}