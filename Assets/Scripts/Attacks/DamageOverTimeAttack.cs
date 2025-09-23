using UnityEngine;
using System.Collections;

// DETECTS HITS!!! Use for DOT Attacks (like laser or flamethrower)
public class DamageOverTimeAttack : MonoBehaviour
{
    private float attackTime;
    private float damage;
    private SpriteRenderer spriteRenderer;

    public void GetData(float atkTime, float dmg)
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

    void OnTriggerStay2D(Collider2D collider)
    {
        if (spriteRenderer != null && spriteRenderer.enabled == false)
        {
            return;
        }

        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
        }
    }
}