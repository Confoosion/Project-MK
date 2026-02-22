using UnityEngine;

public abstract class CharacterSO : ScriptableObject
{
    public string characterName;
    public Sprite characterModel;
    public GameObject attackObject;

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

    // Use this function for coding attacks
    public abstract void UseWeapon(Transform origin, PlayerAttack playerAttack);

    // Use this function for 1st Upgrade
    public abstract void UpgradeT1();

    // Use this function for 2nd Upgrade
    public abstract void UpgradeT2();
}
