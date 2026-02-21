// MeleeAttack.cs
using UnityEngine;

public class MeleeAttack : AttackBase
{
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackArc = 90f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject slashEffectPrefab;
    
    private float critChance = 0f;
    
    public override void Execute(Transform origin)
    {
        if (!CanAttack()) return;
        
        lastAttackTime = Time.time;
        
        // Spawn visual effect
        if (slashEffectPrefab != null)
        {
            Instantiate(slashEffectPrefab, origin.position, origin.rotation);
        }
        
        // Detect enemies in arc
        Collider[] hits = Physics.OverlapSphere(origin.position, attackRange, enemyLayer);
        
        foreach (var hit in hits)
        {
            Vector3 directionToTarget = (hit.transform.position - origin.position).normalized;
            float angle = Vector3.Angle(origin.forward, directionToTarget);
            
            if (angle < attackArc / 2f)
            {
                float damage = baseDamage;
                
                // Apply crit chance
                if (Random.value < critChance)
                {
                    damage *= 2f;
                }
                
                // var enemy = hit.GetComponent<IHealth>();
                // enemy?.TakeDamage(damage);
            }
        }
    }
    
    protected override void ApplyUpgradeStats(int tier)
    {
        switch (tier)
        {
            case 1: // First upgrade
                baseDamage *= 1.5f;
                attackRange *= 1.3f;
                break;
            case 2: // Second upgrade
                critChance = 0.25f; // 25% crit chance
                baseDamage *= 1.4f;
                break;
        }
    }
}