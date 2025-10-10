using UnityEngine;

public class BasicEnemyMovement : EnemyController
{

    public GameObject direction;
    private float speed;
    [SerializeField] private bool cannotMove;
    [SerializeField] private LayerMask wallLayer;



    void Start()
    {
        speed = enemyType.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cannotMove)
        {
            checkMovement();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((wallLayer.value & 1 << collision.transform.gameObject.layer) != 0)
        {
            flipDirection();
        }

        
    }

    private void flipDirection()
    {
        if (direction.transform.rotation.eulerAngles.z == 90)
        {
            setMoveDirection(true);
        }
        else if (direction.transform.rotation.eulerAngles.z == 270)
        {
            setMoveDirection(false);
        }
    }

    private void checkMovement()
    {
        if (direction.transform.rotation.eulerAngles.z == 90)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (direction.transform.rotation.eulerAngles.z == 270)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }

    public void setMoveDirection(bool moveRight)
    {
        if (moveRight)
        {
            direction.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else
        {
            direction.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}


