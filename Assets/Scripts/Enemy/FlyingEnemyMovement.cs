using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemyMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float yRangeMax = 6.5f;
    [SerializeField] private float yRangeMix = 3;
    [SerializeField] private float xRange = 8;

    public Transform target;
    NavMeshAgent agent;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
    
    
}
