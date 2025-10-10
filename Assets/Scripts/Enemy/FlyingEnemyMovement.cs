using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlyingEnemyMovement : EnemyController
{

    [SerializeField] private Transform playerPosition;
    private Vector2 sidePlayerIsOn;

    [SerializeField] private float maxRange;
    [SerializeField] private float minRange;
    [SerializeField] private float movementBuffer;
    private bool canMove;

    void Start()
    {
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector2 newPos = new Vector2();
        if (canMove)
        {
            
            float initialX = transform.position.x;
            float initialY = transform.position.y;

            sidePlayerIsOn = determinePlayerPos();
            if (sidePlayerIsOn.x == 1) //right side, move right
            {
                newPos.x = getRandomDistanceAway() + initialX;
            }
            else //left side, move left
            {
                newPos.x = -getRandomDistanceAway() + initialX;
            }

            if (sidePlayerIsOn.y == 1)
            {
                newPos.y = getRandomDistanceAway() + initialY;
            } 
            else
            {
                newPos.y = -getRandomDistanceAway() + initialY;
            }

            canMove = false;
            Debug.Log(newPos);
        }

        float step = enemyType.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, newPos, step);
    }
    
    IEnumerator waitToMove()
    {
        yield return new WaitForSeconds(movementBuffer);
        canMove = true;
    }

    private Vector2 determinePlayerPos()
    {
        Vector2 playerPos = new Vector2();
        if (playerPosition.position.x > transform.position.x)
        {
            playerPos.x = 1; //right of FE
        }
        else
        {
            playerPos.x = 0; //left of FE
        }

        if (playerPosition.position.y > transform.position.y)
        {
            playerPos.y = 1; //above FE
        }
        else
        {
            playerPos.y = 0; //below FE
        }

        return playerPos;
    }

    private float getRandomDistanceAway()
    {
        return Random.Range(minRange, maxRange);
    }
    

}
