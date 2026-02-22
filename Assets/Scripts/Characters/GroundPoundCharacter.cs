using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Ground Pound Character")]
public class GroundPoundCharacter : CharacterSO
{
    [SerializeField] private float groundPoundForce;
    private GameObject groundPoundAttack;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        if (groundPoundAttack == null)
        {
            groundPoundAttack = Instantiate(attackObject, origin.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, origin);
            groundPoundAttack.GetComponent<MeleeAttack>().SetData(attackPower, attackDuration);

            playerAttack.GetPlayerControl().GroundPoundMovement(groundPoundForce);
        }
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
