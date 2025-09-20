using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private CharacterSO character;
    [SerializeField] private float baseAttackPower;

    void Start()
    {
        character.EquipCharacter();
    }


    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void Attack()
    {
        character.UseWeapon(this.transform);
    }
}
