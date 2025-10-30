using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Archer Character")]
public class ArcherCharacter : CharacterSO
{
    [SerializeField] float projectileForce;
    private bool destroyArrowOnTerrain = true;

    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(0f, 0.25f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 180f : 0f)));

        atk.GetComponent<RangeAttack>().SetData(attackPower, destroyArrowOnTerrain);
        atk.GetComponent<Rigidbody2D>().AddForce(((direction == 1) ? Vector3.right : Vector3.left) * projectileForce, ForceMode2D.Impulse);
    } 
}
