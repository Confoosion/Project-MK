using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks that move forward in a constant velocity
public class ProjectileAttack : MonoBehaviour
{
    private float damage;
    private float speed;
    private float facing;

    private Rigidbody2D rb;

    public void GetData(float dmg, float vel, float direction)
    {
        damage = dmg;
        speed = vel;
        facing = direction;

        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
            Destroy(this.gameObject);
        }
        else if (collider.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(speed * facing, 0f);
    }
}
