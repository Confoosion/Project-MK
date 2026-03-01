using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Burst Projectile Character")]
public class BurstProjectileCharacter : CharacterSO
{
    [Header("Burst Projectile Stats")]
    [SerializeField] private int burstCount;
    [SerializeField] private float burstInterval;
    [SerializeField] private float bulletVelocity;
    [SerializeField] private float speedBoost;
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        // Kinda had to "cheat" and use the PlayerAttack script since this kind of timing requires Monobehavior from just one call of UseWeapon
        // This character should be the only one to "cheat"
        playerAttack.BurstAttack(attackObject, burstCount, burstInterval, bulletVelocity, attackPower);
        if(speedBoost > 0)
            playerAttack.GetPlayerControl().GainSpeedBoost(speedBoost);
    }
}
