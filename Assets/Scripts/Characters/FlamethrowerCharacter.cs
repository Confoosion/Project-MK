using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Flamethrower Character")]
public class FlamethrowerCharacter : CharacterSO
{
    [SerializeField] private float emitterOffset;
    private GameObject atk;
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        if (atk == null)
        {
            atk = Instantiate(attackObject, origin.position + new Vector3(direction * emitterOffset, 0f, 0f), Quaternion.identity, origin);
        }
        atk.GetComponent<ParticleAttack>().SetData(attackPower, attackDuration, origin);
    }
}
