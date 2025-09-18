using UnityEngine;

[CreateAssetMenu(menuName = "Perks/Speed Up")]
public class SpeedPerk : PerkSO
{
    public float speedIncrease = 2f;

    public override void ApplyPerk()
    {
        PerksManager.Singleton.IncreaseSpeed(speedIncrease);
    }
}
