using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    private float attackTime;
    private float damage;

    public void GetData(float atkTime, float dmg)
    {
        attackTime = atkTime;
        damage = dmg;
        StartCoroutine(AttackCountdown());
    }

    IEnumerator AttackCountdown()
    {
        yield return new WaitForSeconds(attackTime);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
        }
    }
}
