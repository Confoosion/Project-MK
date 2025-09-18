using UnityEngine;

[CreateAssetMenu(menuName = "Perks/Double Jump")]
public class DoubleJumpPerk : PerkSO
{
    public int jumps = 1;

    public override void ApplyPerk()
    {
        PerksManager.Singleton.AddJumps(jumps);
    }
}
