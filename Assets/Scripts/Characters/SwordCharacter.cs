using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Sword Character")]
public class SwordCharacter : CharacterSO
{
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction, 0f, 0f), Quaternion.identity, origin);
        atk.GetComponent<MeleeAttack>().SetData(attackPower, attackDuration);
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
