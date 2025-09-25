using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CharacterSO character;
    [SerializeField] private float baseAttackPower;
    public float attackCD;

    private bool canAttack = true;

    void Start()
    {
        character.EquipCharacter();
    }


    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (canAttack)
        {
            character.UseWeapon(this.transform, this);
            canAttack = false;
            StartCoroutine(AttackCooldown(attackCD));
        }
    }

    IEnumerator AttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }

    public void BurstAttack(GameObject atkObject, int count, float interval, float burstVelocity, float atkPower)
    {
        StartCoroutine(DoBurstAttack(atkObject, count, interval, burstVelocity, atkPower));
    }

    IEnumerator DoBurstAttack(GameObject atkObject, int count, float interval, float burstVelocity, float atkPower)
    {
        for (int i = 0; i < count; i++)
        {
            float direction = (transform.localScale.x == 1) ? -1f : 1f;
            GameObject atk = Instantiate(atkObject, transform.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 0f : 180f)));
            atk.GetComponent<ProjectileAttack>().GetData(atkPower, burstVelocity, direction);

            yield return new WaitForSeconds(interval);
        }
    }
}
