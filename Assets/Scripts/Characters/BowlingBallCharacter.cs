using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Bowling Ball Character")]
public class BowlingBallCharacter : CharacterSO
{
    [SerializeField] private float rollingSpeed;
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 0f : 180f)));

        // atk.GetComponent<ProjectileAttack>().GetData(0f, bulletVelocity, direction);
        // atk.GetComponent<ProjectileAttack>().GetImpactData(attackDuration, attackPower);
    }
}
