// CharacterController.cs
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    
    private IAttack currentAttack;
    private int upgradeLevel = 0;
    
    // Stats
    private float currentHealth;
    private float currentMoveSpeed;
    
    private void Start()
    {
        if (characterData != null)
        {
            InitializeCharacter(characterData);
        }
    }
    
    public void InitializeCharacter(CharacterData data)
    {
        characterData = data;
        
        // Remove old attack component if exists
        if (currentAttack != null && currentAttack is MonoBehaviour oldAttack)
        {
            Destroy(oldAttack);
        }
        
        // Add new attack component
        if (data.attackPrefab != null)
        {
            var attackComponent = gameObject.AddComponent(data.attackPrefab.GetType()) as AttackBase;
            
            // Copy values from prefab (you might need to use reflection or serialization for this)
            CopyComponent(data.attackPrefab, attackComponent);
            currentAttack = attackComponent;
        }
        
        // Initialize stats
        currentHealth = data.baseHealth;
        currentMoveSpeed = data.baseMoveSpeed;
        upgradeLevel = 0;
        
        // Instantiate character model if needed
        // (Handle this based on your needs)
    }
    
    public void Attack()
    {
        currentAttack?.Execute(transform);
    }
    
    public void UpgradeCharacter()
    {
        if (upgradeLevel >= 2) return; // Max 2 upgrades
        
        upgradeLevel++;
        CharacterUpgrade upgrade = upgradeLevel == 1 ? 
            characterData.tier1Upgrade : characterData.tier2Upgrade;
        
        // Apply stat upgrades
        currentHealth *= upgrade.healthMultiplier;
        currentMoveSpeed *= upgrade.moveSpeedMultiplier;
        
        // Upgrade attack
        currentAttack?.Upgrade(upgradeLevel);
        
        Debug.Log($"Upgraded {characterData.characterName} to tier {upgradeLevel}: {upgrade.upgradeName}");
    }
    
    private void CopyComponent(Component source, Component destination)
    {
        // Simple field copying - for production, use a more robust solution
        var fields = source.GetType().GetFields(
            System.Reflection.BindingFlags.Public | 
            System.Reflection.BindingFlags.NonPublic | 
            System.Reflection.BindingFlags.Instance
        );
        
        foreach (var field in fields)
        {
            if (!field.IsStatic)
            {
                field.SetValue(destination, field.GetValue(source));
            }
        }
    }
    
    // Runtime switching
    public void SwitchCharacter(CharacterData newData)
    {
        InitializeCharacter(newData);
    }
}