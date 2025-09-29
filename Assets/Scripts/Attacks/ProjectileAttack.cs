using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks that move forward in a constant velocity
public class ProjectileAttack : MonoBehaviour
{
    private float damage;
    private float speed;
    private float facing;

    private Rigidbody2D rb;

    public GameObject impactObject;
    private float impactDamage;
    private float impactDuration;

    public void GetData(float dmg, float vel, float direction)
    {
        damage = dmg;
        speed = vel;
        facing = direction;

        rb = GetComponent<Rigidbody2D>();
    }

    public void GetImpactData(float dmg, float dur)
    {
        impactDamage = dmg;
        impactDuration = dur;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
            SpawnImpact();
            Destroy(this.gameObject);
        }
        else if (collider.CompareTag("Terrain"))
        {
            SpawnImpact();
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed * facing, 0f);
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
