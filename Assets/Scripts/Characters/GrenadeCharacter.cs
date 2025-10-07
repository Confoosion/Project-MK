using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Grenade Character")]
public class GrenadeCharacter : CharacterSO
{
    [SerializeField] float projectileForce;
    private bool destroyOnTerrain = true;

    [SerializeField] float impactDamage;
    [SerializeField] float impactDuration;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(0f, 0.25f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 180f : 0f)));

        atk.GetComponent<RangeAttack>().GetData(attackPower, destroyOnTerrain);
        atk.GetComponent<RangeAttack>().GetImpactData(impactDamage, impactDuration);
        atk.GetComponent<Rigidbody2D>().AddForce(new Vector3(direction, 0.5f, 0f) * projectileForce, ForceMode2D.Impulse);
    } 
}
