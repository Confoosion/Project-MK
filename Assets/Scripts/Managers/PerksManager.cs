using UnityEngine;
using System.Collections.Generic;

public class PerksManager : MonoBehaviour
{
    public static PerksManager Singleton { get; private set; }

    [SerializeField] private PlayerControl playerControl;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private int maxActivePerks = 1; // Change to 2 whenever you're ready

    private CharacterSO character;
    private List<PerkSO> activePerks = new List<PerkSO>();

    void Awake()
    {
        if (Singleton == null)
            Singleton = this;

        if (playerAttack.GetCharacter() != null)
            character = playerAttack.GetCharacter();
    }

    // Returns true if successful
    public bool EquipPerk(PerkSO newPerk, int slot = 0)
    {
        if (slot < 0 || slot >= maxActivePerks)
        {
            Debug.LogWarning($"Invalid perk slot: {slot}");
            return false;
        }

        // Pad list to fit slots
        while (activePerks.Count <= slot)
            activePerks.Add(null);

        // Don't re-equip the same perk
        if (activePerks[slot] == newPerk) return false;

        // Unequip whatever is currently in that slot
        if (activePerks[slot] != null)
            UnapplyPerk(activePerks[slot]);

        activePerks[slot] = newPerk;
        ApplyPerk(newPerk);
        return true;
    }

    public void UnequipPerk(int slot = 0)
    {
        if (slot >= activePerks.Count || activePerks[slot] == null) return;

        UnapplyPerk(activePerks[slot]);
        activePerks[slot] = null;
    }

    public PerkSO GetActivePerk(int slot = 0)
    {
        if (slot >= activePerks.Count) return null;
        return activePerks[slot];
    }

    public bool IsEquipped(PerkSO perk) => activePerks.Contains(perk);

    private void ApplyPerk(PerkSO perk)
    {
        switch (perk.perkType)
        {
            case PerkType.DoubleJump:
                playerControl.extraJumps += (int)perk.value;
                break;
            case PerkType.Speed:
                playerControl.MAXSPEED += perk.value;
                break;
            // case PerkType.Damage:
            //     if (character != null)
            //         character.attackPower *= perk.value;
            //     break;
            // case PerkType.SlowEnemies:
            //     EnemyManager.Singleton.ApplySlowToAll(perk.value);
            //     break;
        }
    }

    private void UnapplyPerk(PerkSO perk)
    {
        switch (perk.perkType)
        {
            case PerkType.DoubleJump:
                playerControl.extraJumps -= (int)perk.value;
                break;
            case PerkType.Speed:
                playerControl.MAXSPEED -= perk.value;
                break;
            // case PerkType.Damage:
            //     if (character != null)
            //         character.attackPower /= perk.value; // Reverse the multiply
            //     break;
            // case PerkType.SlowEnemies:
            //     EnemyManager.Singleton.RemoveSlowFromAll(perk.value);
            //     break;
        }
    }
}