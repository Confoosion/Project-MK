using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Burst Projectile Character")]
public class BurstProjectileCharacter : CharacterSO
{
    [SerializeField] private int burstCount;
    [SerializeField] private float burstInterval;
    [SerializeField] private float bulletVelocity;
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        // Kinda had to "cheat" and use the PlayerAttack script since this kind of timing requires Monobehavior from just one call of UseWeapon
        // This character should be the only one to "cheat"
        playerAttack.BurstAttack(attackObject, burstCount, burstInterval, bulletVelocity, attackPower);
    }
}
