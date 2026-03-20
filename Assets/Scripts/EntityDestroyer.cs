using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerControl playerControl = collider.GetComponent<PlayerControl>();
            if (playerControl != null) 
            {
                playerControl.playerDeath();
            }
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
