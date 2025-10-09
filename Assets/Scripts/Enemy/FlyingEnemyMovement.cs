using System;
using NavMeshPlus.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FlyingEnemyMovement : MonoBehaviour
{

    [SerializeField] private float yRangeMax = 6.5f;
    [SerializeField] private float yRangeMix = 3;
    [SerializeField] private float xRange = 8;

    public Transform player;
    NavMeshAgent agent;


    //hovering 
    public float amplitude = 1.0f;
    public float frequency = 10.0f;
    public float speed = 2.0f;
    private float timeElapsed = 0f;
    private float maxTime = 222200000.0f;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(player.position);


    }

    void FixedUpdate()
    {
        // if (getXDistance() > 8 || getYDistance() < 3 || getYDistance() > 6.5)
        // {
        //     findPlayer();

        // }
        // else //in range to hover / dive
        // {
        //     agent.isStopped = true;
        //     Debug.Log("flyhing enemy in rnage to hover / dive");
        //     hover();

        // }

        hover();
    }


    private float getXDistance()
    {
        return Math.Abs(transform.position.x - player.position.x);
    }

    private float getYDistance()
    {
        return transform.position.y - player.position.y;
    }

    private void findPlayer()
    {
        Vector2 newPlayerPos = new Vector2(player.position.x, player.position.y + 4);
        agent.SetDestination(newPlayerPos);
    }

    private void hover() {
        
    
        timeElapsed = Time.deltaTime * speed;
        float newX = transform.position.x + Time.deltaTime * speed;
        float newY = Mathf.Cos(timeElapsed * frequency) * amplitude;

        transform.position = new Vector2(newX, newY);
        
        Debug.Log("finished hovering");
    }


}
