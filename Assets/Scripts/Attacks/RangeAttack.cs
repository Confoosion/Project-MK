using UnityEngine;

// DETECTS HITS!!! Use for range/projectile attacks
public class RangeAttack : MonoBehaviour
{
    private float damage;
    private bool destroyOnTerrain;

    public void GetData(float dmg, bool removeFromGround)
    {
        damage = dmg;
        destroyOnTerrain = removeFromGround;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy!");
            Destroy(this.gameObject);
        }
        else if (destroyOnTerrain && collider.CompareTag("Terrain"))
        {
            Destroy(this.gameObject);
        }
    }
}
