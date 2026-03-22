// MeleeAttack.cs
using UnityEngine;
using System.Collections;

// DETECTS HITS!!! Use for melee/player-connected attacks
public class MeleeAttack : MonoBehaviour
{
    private float attackTime;
    private float damage;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private bool BOUNCE_ON_IT;
    [SerializeField] private float bounceForce;
    [SerializeField] private AudioClip hitSFX;

    public void SetData(float dmg, float atkTime)
    {
        attackTime = atkTime;
        damage = dmg;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (attackTime > 0f)
            StartCoroutine(AttackCountdown());
    }

    IEnumerator AttackCountdown()
    {
        yield return new WaitForSeconds(attackTime);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (spriteRenderer != null && spriteRenderer.enabled == false)
        {
            return;
        }

        if (collider.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<EnemyController>().enemyTakeDamage(damage);
            // Debug.Log("Hit enemy!");

            SoundManager.Singleton.PlayAttackAudio(hitSFX);

            if (BOUNCE_ON_IT)
            {   // Bounces on enemies, so we will destroy this object and force the player up
                transform.parent.GetComponent<PlayerControl>().BounceFromGroundPound(bounceForce);
                Destroy(this.gameObject);
            }
        }
        else if (collider.CompareTag("Terrain") && BOUNCE_ON_IT)
        {   // Should not be able to bounce on the ground, so we will destroy this object
            Destroy(this.gameObject);
        }
    }
}