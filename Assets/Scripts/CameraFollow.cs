using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private float FollowSpeed = 2f;
    [SerializeField] private float yOffSet = 1f;
    [SerializeField] private Transform playerTransform;


    void Update()
    {
        Vector3 newPos = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffSet, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
