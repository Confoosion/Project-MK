using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Bowling Ball Character")]
public class BowlingBallCharacter : CharacterSO
{
    private GameObject bowlingBall;
    [SerializeField] private float rollingSpeed;
    [SerializeField] private int pierceAmount;
    
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        if (bowlingBall == null)
        {
            float direction = (origin.localScale.x == 1) ? -1f : 1f;
            bowlingBall = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 0f : 180f)));

            bowlingBall.GetComponent<ProjectileAttack>().SetData(attackPower, rollingSpeed, direction, pierceAmount);
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
