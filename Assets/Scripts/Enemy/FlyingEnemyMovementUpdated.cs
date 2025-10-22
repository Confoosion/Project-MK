using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class FlyingEnemyMovementUpdated : EnemyController
{

    [SerializeField] private Transform playerPosition;
    [SerializeField] private LayerMask collisionLayerMask;
    private float speed;

    private Vector2 angleBetween;
    private float angleInRad;
    private Vector2 newDirection;

    private RaycastHit2D groundAndWallDetection;


    [SerializeField] private List<GameObject> middleObjects;




    void Start()
    {
        speed = enemyType.speed;


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
        groundAndWallDetection = Physics2D.Raycast(transform.position, newDirection, 2, collisionLayerMask);

        if (!groundAndWallDetection)
        {
            Debug.Log("Moving towards player now:))");
            goTowardsPosition(playerPosition);
        }
        else if (groundAndWallDetection)
        {
            Transform nearestMiddle = findNearestMiddleObject(middleObjects);
            goTowardsPosition(nearestMiddle);

            // if (groundAndWallDetection.collider.gameObject.layer == 6)
            // {
            //     if (needDirection)
            //     {
            //         if (transform.position.x > playerPosition.position.x)
            //         {
            //             //move left
            //             moveLeft = true;
            //             Debug.Log("moveLeft = true");
            //         }
            //         else if (transform.position.x < playerPosition.position.x)
            //         {
            //             moveLeft = false;
            //             Debug.Log("moveLeft = false:)");
            //         }
            //         needDirection = false;
            //     }
            //     else
            //     {



            //         if (moveLeft)
            //         {
            //             //move left
            //             Debug.Log("Should be moving left");
            //             transform.Translate(Vector2.left * speed * Time.deltaTime);
            //             Vector2 lookRight = new Vector2(transform.position.x + 0.5f, transform.position.y);

            //             if (transform.position.y > playerPosition.position.y)
            //             {
            //                 Debug.Log("player is below flying");
            //                 groundAndWallDetection = Physics2D.Raycast(lookRight, Vector2.down, 1, collisionLayerMask);
            //                 //looking downwards for the ground detection on the right side of the flying object
            //             }
            //             else if (transform.position.y < playerPosition.position.y)
            //             {
            //                 Debug.Log("player is above: left");
            //                 groundAndWallDetection = Physics2D.Raycast(lookRight, Vector2.up, 1, collisionLayerMask);
            //                 //looking upwards for the ground on the right side of the flying object
            //             }
            //         }
            //         else
            //         {
            //             //move right
            //             Debug.Log("moveing righttt");
            //             transform.Translate(Vector2.right * speed * Time.deltaTime);
            //             Vector2 lookLeft = new Vector2(transform.position.x - 0.5f, transform.position.y);
            //             if (transform.position.y > playerPosition.position.y)
            //             {
            //                 Debug.Log("player is below flying");

            //                 groundAndWallDetection = Physics2D.Raycast(lookLeft, Vector2.down, 1, collisionLayerMask);
            //                 //looking downwards for ground on the left side of the lfying object
            //             }
            //             else if (transform.position.y < playerPosition.position.y)
            //             {
            //                 Debug.Log("player is above: right");

            //                 groundAndWallDetection = Physics2D.Raycast(lookLeft, Vector2.up, 1, collisionLayerMask);
            //                 //looking upwards for the ground on the left side of the flying object
            //             }
            //         }
            //     }
            // }

        }


    }



    private void goTowardsPosition(Transform pos)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, pos.position, step);
    }

    private Transform findNearestMiddleObject(List<GameObject> MO)
    {
        float minDistance = 1000;
        Transform tempObj = null;
        foreach (GameObject middleObject in MO)
        {
            if (Vector2.Distance(transform.position, middleObject.transform.position) <= minDistance)
            {
                minDistance = Vector2.Distance(transform.position, middleObject.transform.position);
                tempObj = middleObject.transform;
            }
        }
        return tempObj;

    }

}
