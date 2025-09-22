using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    [SerializeField] float projectileForce;
    private float damage;
    private float direction;
    private Rigidbody2D rb;

    public void GetData(float dmg)
    {
        damage = dmg;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * projectileForce, ForceMode2D.Impulse);
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
}
