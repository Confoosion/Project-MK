// Projectile.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private LayerMask hitLayers;
    
    private float damage = 10f;
    private bool hasHit = false;
    
    private void Start()
    {
        // Destroy projectile after lifetime expires
        Destroy(gameObject, lifetime);
    }
    
    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (hasHit) return;
        
        // Check if we hit something on the correct layer
        if (((1 << other.gameObject.layer) & hitLayers) != 0)
        {
            // Try to damage the target
            // var health = other.GetComponent<IHealth>();
            // if (health != null)
            // {
            //     health.TakeDamage(damage);
            // }
            
            // Spawn hit effect
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
            
            hasHit = true;
            Destroy(gameObject);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        
        // Check if we hit something on the correct layer
        if (((1 << collision.gameObject.layer) & hitLayers) != 0)
        {
            // Try to damage the target
            // var health = collision.gameObject.GetComponent<IHealth>();
            // if (health != null)
            // {
            //     health.TakeDamage(damage);
            // }
            
            // Spawn hit effect
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
            
            hasHit = true;
            Destroy(gameObject);
        }
    }
}