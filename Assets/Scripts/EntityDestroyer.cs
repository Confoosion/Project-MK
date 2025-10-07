using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
