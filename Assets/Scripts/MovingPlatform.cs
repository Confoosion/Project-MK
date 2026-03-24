using System.Runtime.CompilerServices;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 1f;

    private int currentIndex = 0;
    private float waitTimer = 0f;
    private bool waiting = false;
    private Rigidbody2D rb;

    private void Awake()
    {
         rb = GetComponent<Rigidbody2D>();
        transform.position = waypoints[0].position;
    }

    private void FixedUpdate()
    {
        if (waiting)
        {
            waitTimer -= Time.fixedDeltaTime;
            if (waitTimer <= 0f) 
                waiting = false;
            return;
        }

        Transform target = waypoints[currentIndex];
        Vector2 newPos = Vector2.MoveTowards(
            rb.position, 
            target.position, 
            speed * Time.fixedDeltaTime
        );
        rb.MovePosition(newPos);

        if (Vector2.Distance(rb.position, target.position) < 0.05f)
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
            waiting = true;
            waitTimer = waitTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == null) return;
        if (!gameObject.activeInHierarchy) return;
        if (!collision.gameObject.activeInHierarchy) return;

        if (collision.gameObject.CompareTag("Player"))
            collision.transform.SetParent(null);
    }
}
