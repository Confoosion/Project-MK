using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks that move forward in a constant velocity
public class ProjectileAttack : MonoBehaviour
{
    private float damage;
    private float speed;
    private float facing;

    private Rigidbody2D rb;

    [SerializeField] private bool canBounceOffWalls;
    [SerializeField] private bool canBounceOffEnemies;
    [SerializeField] private bool canRollOnGround;
    [SerializeField] private bool physicsObject;
    [SerializeField] private int bounceAmount;
    private int pierceAmount;
    public GameObject impactObject;
    private float impactDamage;
    private float impactDuration;
    private LayerMask layerMask;

    void Awake()
    {
        layerMask = LayerMask.NameToLayer("Wall");
    }

    public void GetData(float dmg, float vel, float direction, int piercing = 0)
    {
        damage = dmg;
        speed = vel;
        facing = direction;
        pierceAmount = piercing;

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
            collider.gameObject.GetComponent<EnemyController>().enemyTakeDamage(damage);
            if (pierceAmount > 0)
            {
                pierceAmount--;
            }
            else if (canBounceOffEnemies && bounceAmount > 0)
            {
                bounceAmount--;
                facing *= -1f;
            }
            else
            {
                ProjectileDespawn();
            }
        }

        if (collider.CompareTag("Terrain"))
        {
            if (canBounceOffWalls && bounceAmount > 0 && collider.gameObject.layer == layerMask)
            {
                bounceAmount--;
                facing *= -1f;
            }
            else if(!canRollOnGround)
            {
                ProjectileDespawn();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed * facing, (physicsObject) ? rb.linearVelocity.y : 0f);
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
