using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using Random = UnityEngine.Random;

public class FlyingEnemyMovement : EnemyController
{

    [SerializeField] private Transform playerPosition;
    private float speed;
    
    [SerializeField] private List<GameObject> middleObjects;

    private bool playerInRange;
    private bool roaming;
    private int middleObjectIndex = 0;




    void Start()
    {
        speed = enemyType.speed;

        playerPosition = GameObject.Find("Player").transform;
        GameObject tmp = GameObject.Find("FlyingEnemyMarkers");

        for(int i = 0; i < tmp.transform.childCount; i++)
        {
            middleObjects.Add(tmp.transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {

        if (!playerInRange) //could not find player
        {
            if (!roaming) //going towards nearest point before roaming around
            {
                Transform nearestMiddleObj = findNearestMiddleObject(middleObjects);
                goTowardsPosition(nearestMiddleObj);
                if (checkIfClose(nearestMiddleObj))
                {
                    roaming = true;
                    increaseMiddleObjectIndex();
                }
            }
            else // need to be roaming around
            {
                goTowardsPosition(middleObjects[middleObjectIndex].transform);
                if (checkIfClose(middleObjects[middleObjectIndex].transform))
                {
                    increaseMiddleObjectIndex();
                }
                //to go to next point
                //once close enough, go towards the next point 
                //make sure that the index doens't go out of bounds
            }
        }
        else
        {
            goTowardsPosition(playerPosition);
            roaming = false;
        }

        //choose point damn state machines
        //While player in range, go towards player
        //when player is out of range, 
        //  I need you to go to the first ... no because depending on the spawn point, it changes. SOOO 
        //Find nearest point, go towards it, roaming = true
        //Loop through list to go to each point. 
        //

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            playerInRange = false;
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
        int index = 0;


        foreach (GameObject middleObject in MO)
        {

            if (Vector2.Distance(transform.position, middleObject.transform.position) <= minDistance)
            {
                minDistance = Vector2.Distance(transform.position, middleObject.transform.position);
                tempObj = middleObject.transform;
                middleObjectIndex = index;
            }
            index++;
        }
        return tempObj;

    }

    private bool checkIfClose(Transform obj)
    {
        if (Vector2.Distance(transform.position, obj.position) <= 0.2f)
        {
            return true;
        }
        return false;
    }

    private void increaseMiddleObjectIndex()
    {
        middleObjectIndex++;
        if (middleObjectIndex >= middleObjects.Count)
        {
            middleObjectIndex = 0;
        }
    }

}
