using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks that move forward in a constant velocity
public class ProjectileAttack : MonoBehaviour
{
    private float damage;
    private float speed;
    private float facing;

    private Rigidbody2D rb;

    [SerializeField] private bool canBounceOffWalls;
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
            ProjectileDespawn();
            return;
        }

        if (collider.CompareTag("Wall"))
        {
            if (canBounceOffWalls)
            {
                facing *= -1f;
            }
            else
            {
                ProjectileDespawn();
            }
        }
        else if (collider.CompareTag("Terrain"))
        {
            ProjectileDespawn();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed * facing, 0f);
    }

    private void ProjectileDespawn()
    {
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
