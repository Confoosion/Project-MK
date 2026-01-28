using UnityEngine;

// IAttack.cs
public interface IAttack
{
    void Execute(Transform origin);
    void Upgrade(int tier);
    float GetCooldown();
}