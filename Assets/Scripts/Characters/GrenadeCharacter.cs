using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Grenade Character")]
public class GrenadeCharacter : CharacterSO
{
    [Header("Grenade Stats")]
    [SerializeField] float projectileForce;
    private bool destroyOnTerrain = true;

    [SerializeField] float impactDamage;
    [SerializeField] float impactDuration;
    [SerializeField] int burstCount = 1;
    [SerializeField] float burstRate;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;

        if(burstCount <= 1)
        {
            GameObject atk = Instantiate(attackObject, origin.position + new Vector3(0f, 0.25f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 180f : 0f)));

            atk.GetComponent<RangeAttack>().SetData(attackPower, destroyOnTerrain);
            atk.GetComponent<RangeAttack>().SetImpactData(impactDamage, impactDuration);
            atk.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0.5f, 0f) * projectileForce, ForceMode2D.Impulse);
        }
        else
        {
            playerAttack.BurstAttack(attackObject, burstCount, burstRate, 0f, attackPower, 
                                     new Vector2(1f, 0.5f) * projectileForce, destroyOnTerrain, impactDamage, impactDuration);
        }
    }
}
