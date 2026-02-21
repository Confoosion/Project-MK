// CharacterData.cs
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Identity")]
    public string characterName;
    public Sprite icon;
    public GameObject prefab; // Character model/prefab
    
    [Header("Stats")]
    public float baseHealth = 100f;
    public float baseMoveSpeed = 5f;
    
    [Header("Attack")]
    public AttackBase attackPrefab; // The attack component to add
    
    [Header("Upgrades")]
    public CharacterUpgrade tier1Upgrade;
    public CharacterUpgrade tier2Upgrade;
}

[System.Serializable]
public class CharacterUpgrade
{
    public string upgradeName;
    [TextArea] public string description;
    
    // Stat modifiers
    public float healthMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
    
    // Attack upgrade is handled by the attack script itself
}