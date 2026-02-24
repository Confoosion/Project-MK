using UnityEngine;
using System.Collections;

public abstract class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite characterModel;
    public GameObject attackObject;

    [Header("Basic Stats")]
    public int level = 1;
    public float attackPower;
    public float attackDuration;
    public float attackCD;

    public void EquipCharacter()
    {
        if (characterModel == null)
        {
            return;
        }

        CharacterManager.Singleton.ChangeCharacter(characterModel, attackCD);
    }

    // Use this function for coding attacks
    public abstract void UseWeapon(Transform origin, PlayerAttack playerAttack);
}
