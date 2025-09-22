using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite characterModel;

    [Header("Basic Stats")]
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

    // Use this class for coding attacks
    public abstract void UseWeapon(Transform origin);
}
