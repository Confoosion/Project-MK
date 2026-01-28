// AttackBase.cs
using UnityEngine;

public abstract class AttackBase : MonoBehaviour, IAttack
{
    [SerializeField] protected float baseCooldown = 1f;
    [SerializeField] protected float baseDamage = 10f;
    
    protected int currentTier = 0;
    protected float lastAttackTime;
    
    public abstract void Execute(Transform origin);
    
    public virtual void Upgrade(int tier)
    {
        currentTier = tier;
        ApplyUpgradeStats(tier);
    }
    
    protected abstract void ApplyUpgradeStats(int tier);
    
    public float GetCooldown() => baseCooldown;
    
    protected bool CanAttack()
    {
        return Time.time >= lastAttackTime + baseCooldown;
    }
}