using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Characters/Laser Gun Character")]
public class LaserCharacter : CharacterSO
{
    [SerializeField] private GameObject laserObject;

    public override void UseWeapon(Transform origin)
    {
        float direction = (origin.localScale.x == 1) ? -2.95f : 2.95f;
        GameObject atk = Instantiate(laserObject, origin.position + new Vector3(direction, 0f, 0f), Quaternion.identity, origin);

        atk.GetComponent<DelayedAttack>().GetData(attackPower, attackDuration * 0.5f, attackDuration * 0.5f);
        atk.GetComponent<DamageOverTimeAttack>().GetData(0f, attackPower);
    }
}
