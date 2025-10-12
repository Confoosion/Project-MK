using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingEnemyMovement : EnemyController
{

    [SerializeField] private Transform playerPosition;
    [SerializeField] private float maxRange;
    [SerializeField] private LayerMask collisionLayerMask;
    [SerializeField] private GameObject facing;
    private bool canMove;
    private float speed;
    private Vector2 initialPos;

    private Vector2 angleBetween;
    private float angleInRad;
    private Vector2 newDirection;
    private bool initialDistanceFound = false;

    void Start()
    {
        canMove = false;
        speed = enemyType.speed;
        initialPos = transform.position;
        
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
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
            else if (groundAndWallDetection.collider.gameObject.layer == 8)
            {
                //Debug.Log("hit wall");
                
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
            if (Vector2.Distance(initialPos, transform.position) >= maxRange)
            {

                Debug.Log("moved 5 units");
                initialDistanceFound = false;
            }
        }




    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit:THE WALL???" + collision.gameObject);
        if(collision.gameObject.layer == 8)
        {
            Debug.Log("hit wal with collider");
        }
    }


    private void flipDirection()
    {
        if (facing.transform.rotation.eulerAngles.z == 90)
        {
            setMoveDirection(true);
        }
        else if (facing.transform.rotation.eulerAngles.z == 270)
        {
            setMoveDirection(false);
        }
    }

    private void checkMovement()
    {
        if (facing.transform.rotation.eulerAngles.z == 90)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (facing.transform.rotation.eulerAngles.z == 270)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
    
    private void setMoveDirection(bool moveRight)
    {
        if (moveRight)
        {
            facing.transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else
        {
            facing.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

}
