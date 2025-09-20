using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Sword Character")]
public class SwordCharacter : CharacterSO
{
    [SerializeField] private GameObject hitbox;

    public override void UseWeapon(Transform origin)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(hitbox, origin.position + new Vector3(direction, 0f, 0f), Quaternion.identity, origin);
        atk.GetComponent<MeleeAttack>().GetData(attackDuration, attackPower);
    }
}
