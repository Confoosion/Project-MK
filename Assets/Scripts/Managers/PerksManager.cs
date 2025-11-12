using UnityEngine;

public class PerksManager : MonoBehaviour
{
    public static PerksManager Singleton { get; private set; }

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private PlayerAttack playerAttack;

    private CharacterSO character;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
        if (playerAttack.GetCharacter() != null)
        {
            character = playerAttack.GetCharacter();
        }
    }

    public void AddJumps(int num)
    {
        playerControl.extraJumps = num;
    }

    public void IncreaseSpeed(float num)
    {
        playerControl.speed += num;
    }

    public void IncreaseDamage(float dmgMultiplier)
    {
        if (character)
        {
            character.attackPower *= dmgMultiplier;
        }
    }
}
