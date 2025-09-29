using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Machine Gun Character")]
public class MachineGunCharacter : CharacterSO
{
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float weaponKnockback;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 0f : 180f)));
        atk.GetComponent<ProjectileAttack>().GetData(attackPower, bulletVelocity, direction);

        // Push player back
        float push = weaponKnockback * -direction;
        origin.position = new Vector2(origin.position.x + push, origin.position.y);
    }
}
