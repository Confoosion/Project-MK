using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Shotgun Character")]
public class ShotgunCharacter : CharacterSO
{
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 0f : 180f)));
        atk.transform.GetChild(0).GetComponent<ParticleAttack>().SetData(attackPower, attackDuration, origin);
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
