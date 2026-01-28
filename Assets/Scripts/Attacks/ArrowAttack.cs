// ArrowAttack.cs
using UnityEngine;

public class ArrowAttack : AttackBase
{
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private float arrowSpeed = 20f;
    [SerializeField] private int arrowCount = 1;
    
    public override void Execute(Transform origin)
    {
        if (!CanAttack()) return;
        
        lastAttackTime = Time.time;
        
        for (int i = 0; i < arrowCount; i++)
        {
            float angle = (i - (arrowCount - 1) / 2f) * 15f; // Spread arrows
            GameObject arrow = Instantiate(arrowPrefab, origin.position, origin.rotation);
            
            Quaternion rotation = Quaternion.Euler(0, angle, 0) * origin.rotation;
            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = rotation * Vector3.forward * arrowSpeed;
            }
            
            // Set damage on projectile
            var projectile = arrow.GetComponent<Projectile>();
            if (projectile != null) projectile.SetDamage(baseDamage);
        }
    }
    
    protected override void ApplyUpgradeStats(int tier)
    {
        switch (tier)
        {
            case 1: // First upgrade
                baseDamage *= 1.5f;
                arrowSpeed *= 1.2f;
                break;
            case 2: // Second upgrade
                arrowCount = 3; // Triple shot
                baseDamage *= 1.3f;
                break;
        }
    }
}