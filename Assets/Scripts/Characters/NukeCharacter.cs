using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Nuke Character")]
public class NukeCharacter : CharacterSO
{
    [SerializeField] int maxUses;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        playerAttack.ActivateNuke(maxUses);
    }

    public override void UpgradeT1()
    {
        return;
    }

    public override void UpgradeT2()
    {
        return;
    }
}
