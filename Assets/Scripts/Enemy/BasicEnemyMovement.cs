using UnityEngine;

public class BasicEnemyMovement : EnemyController
{

    public GameObject direction;
    private float speed;
    [SerializeField] private bool cannotMove;
    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private Transform playerPosition;
    private Vector2 initialPos;

    [SerializeField] private LayerMask collisionLayerMask;
    private bool isFlying;
    private Vector2 angleBetween;
    private float angleInRad;
    private Vector2 newDirection;
    private bool initialDistanceFound = false;
    [SerializeField] private float maxFlyingRange;



    void Start()
    {
        speed = enemyType.speed;
        isFlying = enemyType.isFlying;
    }

    // Update is called once per frame
    void Update()
    {
        if (!cannotMove && !isFlying)
        {
            checkMovement();
        } else if (!cannotMove && isFlying)
        {
            angleBetween = playerPosition.position - transform.position;
            angleInRad = Mathf.Atan2(angleBetween.y, angleBetween.x);
            newDirection = new Vector2(Mathf.Cos(angleInRad), Mathf.Sin(angleInRad));
            RaycastHit2D groundAndWallDetection = Physics2D.Raycast(transform.position, newDirection, 2, collisionLayerMask);
            if (groundAndWallDetection)
            {
                if (groundAndWallDetection.collider.gameObject.layer == 6)
                {
                    Debug.Log("hit ground"); // need to move around the ground;
                    checkMovement();

                }
                else if(groundAndWallDetection.collider.gameObject.layer == 8)
                {
                    Debug.Log("hit wall");
                }
            }
            else
            {
                if (!initialDistanceFound)
                {
                    initialPos = transform.position;
                    initialDistanceFound = true;
                }
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, playerPosition.position, step);
                if(Vector2.Distance(initialPos, transform.position) >= maxFlyingRange)
                {
                    
                    Debug.Log("moved 5 units");
                    initialDistanceFound = false;
                }
            }
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


