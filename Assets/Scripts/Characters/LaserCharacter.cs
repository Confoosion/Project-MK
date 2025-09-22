using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Characters/Laser Gun Character")]
public class LaserCharacter : CharacterSO
{
    [SerializeField] private GameObject laserObject;

    public override void UseWeapon(Transform origin)
    {
        GameObject atk = Instantiate(laserObject, origin.position, Quaternion.identity, origin);

        atk.GetComponent<DelayedAttack>().GetData(attackPower, attackDuration * 0.5f, attackDuration * 0.5f);
        laserObject.transform.GetChild(0).GetComponent<DamageOverTimeAttack>().GetData(0f, attackPower);
    }
}
