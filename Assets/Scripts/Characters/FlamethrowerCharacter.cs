using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Flamethrower Character")]
public class FlamethrowerCharacter : CharacterSO
{
    private GameObject atk;
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        if (atk == null)
        {
            atk = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.identity, origin);
            atk.GetComponent<DamageOverTimeAttack>().GetData(0f, attackPower);
        }
        atk.GetComponent<ParticleControl>().GetData(attackDuration, origin);
    }
}
