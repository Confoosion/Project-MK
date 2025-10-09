using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MuffinSpawner : MonoBehaviour
{
    public static MuffinSpawner Singleton { get; private set; }
    [SerializeField] private GameObject muffin;
    [SerializeField] private float spawnOffset;
    [SerializeField] private float spawnHeight;
    private List<Transform> platforms = new List<Transform>();
    private Transform muffinPlatform;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            for (int i = 0; i < transform.childCount; i++)
            {
                platforms.Add(transform.GetChild(i).transform);
            }
        }
    }

    void Start()
    {
        SpawnMuffin();
    }

    public void SpawnMuffin()
    {
        Transform newMuffinPlatform = platforms[Random.Range(0, platforms.Count)];

        if (muffinPlatform != null)
            platforms.Add(muffinPlatform);

        muffinPlatform = newMuffinPlatform;
        platforms.Remove(newMuffinPlatform);

        Bounds bounds = muffinPlatform.GetComponent<SpriteRenderer>().bounds;
        float randomX = Random.Range(bounds.min.x + spawnOffset, bounds.max.x - spawnOffset);
        float y = bounds.max.y + spawnHeight;

        Instantiate(muffin, new Vector2(randomX, y), Quaternion.identity);

    }
}
