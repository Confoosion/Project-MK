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
            character.UseWeapon(this.transform);
            canAttack = false;
            StartCoroutine(AttackCooldown(attackCD));
        }
    }

    IEnumerator AttackCooldown(float cd)
    {
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }
}
