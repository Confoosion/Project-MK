using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Nuke Character")]
public class NukeCharacter : CharacterSO
{
    [Header("Nuke Stats")]
    [SerializeField] int maxUses;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        playerAttack.ActivateNuke(maxUses);
    }
}
