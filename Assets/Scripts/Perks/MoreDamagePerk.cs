using UnityEngine;

[CreateAssetMenu(menuName = "Perks/MoreDamage")]
public class MoreDamagePerk : PerkSO
{
    public float dmgMultiplier = 1f;

    public override void ApplyPerk()
    {
        PerksManager.Singleton.IncreaseDamage(dmgMultiplier);
    }
}
