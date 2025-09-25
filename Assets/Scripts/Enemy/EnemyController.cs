using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    public EnemySO enemyType;
    public GameObject direction;

    private float facingDirection;

    private void Start()
    {
        
        facingDirection = direction.transform.eulerAngles.z;
    }
    void Update()
    {
        checkMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            flipDirection();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player should be dead");
            //Player should be dead
        }
    }

    private void flipDirection()
    {
        if (direction.transform.rotation.eulerAngles.z == 90)
        {
            direction.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction.transform.rotation.eulerAngles.z == 270)
        {
            direction.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void checkMovement()
    {
        if (direction.transform.rotation.eulerAngles.z == 90)
        {
            transform.Translate(Vector2.left * enemyType.speed * Time.deltaTime);
        }
        else if (direction.transform.rotation.eulerAngles.z == 270)
        {
            transform.Translate(Vector2.right * enemyType.speed * Time.deltaTime);
        }
    }
}
