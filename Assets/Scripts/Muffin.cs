using UnityEngine;

public class Muffin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            MuffinSpawner.Singleton.SpawnMuffin();
            Destroy(this.gameObject);
        }
    }
}
