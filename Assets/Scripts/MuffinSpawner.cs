using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MuffinSpawner : MonoBehaviour
{
    public static MuffinSpawner Singleton { get; private set; }
    [SerializeField] private GameObject muffin;
    [SerializeField] private float spawnOffset = 1.5f;
    [SerializeField] private float spawnHeight = 5f;
    [SerializeField] private List<Transform> platforms = new List<Transform>();
    private Transform muffinPlatform;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void SpawnMuffin()
    {
        if (platforms.Count == 0)
        {
            Debug.LogWarning("No platforms available to spawn muffin!");
            return;
        }
        
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

    /// <summary>
    /// Adds all child platforms from a parent transform (e.g., biome's PLATFORMS object)
    /// </summary>
    public void AddPlatforms(Transform platformParent)
    {
        if (platformParent == null)
        {
            Debug.LogWarning("Cannot add platforms from null parent!");
            return;
        }
        
        int addedCount = 0;
        for(int i = 0; i < platformParent.childCount; i++)
        {
            Transform platform = platformParent.GetChild(i);
            
            // Only add if it has a SpriteRenderer (to ensure it's a valid platform)
            if (platform.GetComponent<SpriteRenderer>() != null)
            {
                platforms.Add(platform);
                addedCount++;
            }
        }
        
        Debug.Log($"Added {addedCount} platforms from {platformParent.name}. Total platforms: {platforms.Count}");
    }
    
    /// <summary>
    /// Clears all platforms (useful for resetting)
    /// </summary>
    public void ClearPlatforms()
    {
        platforms.Clear();
        muffinPlatform = null;
        Debug.Log("Cleared all platforms from MuffinSpawner");
    }
}