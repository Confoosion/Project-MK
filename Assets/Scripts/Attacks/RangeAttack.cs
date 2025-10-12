using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks
public class RangeAttack : MonoBehaviour
{
    private float damage;
    private bool destroyOnTerrain;
    public GameObject impactObject;
    private float impactDamage;
    private float impactDuration;

    public void GetData(float dmg, bool removeFromGround)
    {
        damage = dmg;
        destroyOnTerrain = removeFromGround;
    }

    public void GetImpactData(float dmg, float dur)
    {
        impactDamage = dmg;
        impactDuration = dur;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Debug.Log("COLLIDED WITH " + collider.transform.tag);
        if (collider.CompareTag("Enemy"))
        {
            // Debug.Log("Hit enemy!");
            collider.gameObject.GetComponent<EnemyController>().enemyTakeDamage(damage);

            ProjectileDespawn();
        }
        else if (destroyOnTerrain && collider.CompareTag("Terrain"))
        {
            ProjectileDespawn();
        }
    }

    private void ProjectileDespawn()
    {
        // Debug.Log("Collided");
        SpawnImpact();
        Destroy(this.gameObject);
    }

    private void SpawnImpact()
    {
        if (impactObject != null)
        {
            GameObject impact = Instantiate(impactObject, transform.position, Quaternion.identity);
            if (impact.GetComponent<MeleeAttack>())
            {
                impact.GetComponent<MeleeAttack>().GetData(impactDamage, impactDuration);
            }
        }
    }
}
