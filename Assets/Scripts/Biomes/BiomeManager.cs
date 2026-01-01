using UnityEngine;

public class BiomeManager : MonoBehaviour
{
    [Header("Connectors")]
    public BiomeConnector leftConnector;
    public BiomeConnector rightConnector;
    
    [Header("Platforms")]
    public Transform platformsParent;
    
    private void Awake()
    {
        // Auto-find connectors if not assigned
        if (leftConnector == null || rightConnector == null)
        {
            BiomeConnector[] connectors = GetComponentsInChildren<BiomeConnector>();
            foreach (var connector in connectors)
            {
                if (connector.connectorType == BiomeConnector.ConnectorType.Left)
                    leftConnector = connector;
                else if (connector.connectorType == BiomeConnector.ConnectorType.Right)
                    rightConnector = connector;
            }
        }
        
        // Auto-find platforms parent if not assigned
        if (platformsParent == null)
        {
            platformsParent = transform.Find("PLATFORMS");
            if (platformsParent == null)
            {
                Debug.LogWarning($"BiomeManager on {gameObject.name} could not find PLATFORMS parent object!");
            }
        }
    }
    
    /// <summary>
    /// Registers this biome's platforms with the MuffinSpawner
    /// </summary>
    public void RegisterPlatforms()
    {
        if (platformsParent != null && MuffinSpawner.Singleton != null)
        {
            MuffinSpawner.Singleton.AddPlatforms(platformsParent);
            Debug.Log($"Registered {platformsParent.childCount} platforms from {gameObject.name}");
        }
    }
    
    /// <summary>
    /// Connects another biome to the right of this biome
    /// </summary>
    public void ConnectBiomeToRight(BiomeManager nextBiome)
    {
        if (rightConnector == null || nextBiome.leftConnector == null)
        {
            Debug.LogError("Missing connectors for biome connection!");
            return;
        }
        
        // Calculate offset needed to align connectors
        Vector3 offset = rightConnector.transform.position - nextBiome.leftConnector.transform.position;
        
        // Move the next biome so its left connector aligns with this biome's right connector
        nextBiome.transform.position += offset;
    }
    
    /// <summary>
    /// Connects another biome to the left of this biome
    /// </summary>
    public void ConnectBiomeToLeft(BiomeManager previousBiome)
    {
        if (leftConnector == null || previousBiome.rightConnector == null)
        {
            Debug.LogError("Missing connectors for biome connection!");
            return;
        }
        
        // Calculate offset needed to align connectors
        Vector3 offset = leftConnector.transform.position - previousBiome.rightConnector.transform.position;
        
        // Move the previous biome so its right connector aligns with this biome's left connector
        previousBiome.transform.position += offset;
    }
    
    private void OnDrawGizmos()
    {
        // Draw biome bounds
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
    }
}