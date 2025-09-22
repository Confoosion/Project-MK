using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks
public class RangeAttack : MonoBehaviour
{
    private float damage;

    public void GetData(float dmg)
    {
        damage = dmg;
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
