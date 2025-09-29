using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Characters/Laser Gun Character")]
public class LaserCharacter : CharacterSO
{
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -2.95f : 2.95f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction, 0f, 0f), Quaternion.identity, origin);

        atk.GetComponent<DelayedAttack>().GetData(attackPower, attackDuration * 0.5f, attackDuration * 0.5f);
        atk.GetComponent<DamageOverTimeAttack>().GetData(0f, attackPower);
    }
}
