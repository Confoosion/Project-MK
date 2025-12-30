using System.Collections.Generic;
using UnityEngine;

public class BiomeChainBuilder : MonoBehaviour
{
    [Header("Biome Chain")]
    public List<GameObject> biomePrefabs = new List<GameObject>();
    public Transform biomeContainer;
    
    private List<BiomeManager> instantiatedBiomes = new List<BiomeManager>();
    
    [Header("Starting Position")]
    public Vector3 startPosition = Vector3.zero;
    
    [Header("Progressive Unlock")]
    public bool useProgressiveUnlock = true;
    
    private void Start()
    {
        if (biomeContainer == null)
            biomeContainer = transform;
        
        if (useProgressiveUnlock)
        {
            // Subscribe to score events
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnBiomeUnlocked.AddListener(OnBiomeUnlocked);
            }
            
            // Start with first random biome
            SpawnInitialBiome();
        }
    }
    
    private void OnDestroy()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnBiomeUnlocked.RemoveListener(OnBiomeUnlocked);
        }
    }
    
    private void SpawnInitialBiome()
    {
        if (biomePrefabs.Count == 0)
        {
            Debug.LogWarning("No biome prefabs assigned!");
            return;
        }
        
        // Pick random biome for first one
        int randomIndex = Random.Range(0, biomePrefabs.Count);
        GameObject randomBiome = biomePrefabs[randomIndex];
        
        GameObject biomeObj = Instantiate(randomBiome, biomeContainer);
        BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
        
        if (biome != null)
        {
            biome.transform.position = startPosition;
            instantiatedBiomes.Add(biome);
            Debug.Log($"Spawned initial biome: {randomBiome.name}");
        }
    }
    
    private void OnBiomeUnlocked(int biomeIndex)
    {
        // Spawn next random biome
        if (biomePrefabs.Count == 0) return;
        
        int randomIndex = Random.Range(0, biomePrefabs.Count);
        AddBiomeToChain(biomePrefabs[randomIndex]);
    }
    
    /// <summary>
    /// Builds a chain of biomes from the prefab list
    /// </summary>
    [ContextMenu("Build Biome Chain")]
    public void BuildBiomeChain()
    {
        ClearBiomes();
        
        if (biomePrefabs.Count == 0)
        {
            Debug.LogWarning("No biome prefabs assigned!");
            return;
        }
        
        BiomeManager previousBiome = null;
        
        for (int i = 0; i < biomePrefabs.Count; i++)
        {
            GameObject biomeObj = Instantiate(biomePrefabs[i], biomeContainer);
            BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
            
            if (biome == null)
            {
                Debug.LogError($"Biome prefab {biomePrefabs[i].name} is missing BiomeManager component!");
                Destroy(biomeObj);
                continue;
            }
            
            instantiatedBiomes.Add(biome);
            
            // First biome goes at start position
            if (i == 0)
            {
                biome.transform.position = startPosition;
            }
            else if (previousBiome != null)
            {
                // Connect to previous biome
                previousBiome.ConnectBiomeToRight(biome);
            }
            
            previousBiome = biome;
        }
        
        Debug.Log($"Built chain of {instantiatedBiomes.Count} biomes");
    }
    
    /// <summary>
    /// Adds a new biome to the end of the chain
    /// </summary>
    public BiomeManager AddBiomeToChain(GameObject biomePrefab)
    {
        GameObject biomeObj = Instantiate(biomePrefab, biomeContainer);
        BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
        
        if (biome == null)
        {
            Debug.LogError($"Biome prefab {biomePrefab.name} is missing BiomeManager component!");
            Destroy(biomeObj);
            return null;
        }
        
        if (instantiatedBiomes.Count > 0)
        {
            BiomeManager lastBiome = instantiatedBiomes[instantiatedBiomes.Count - 1];
            lastBiome.ConnectBiomeToRight(biome);
        }
        else
        {
            biome.transform.position = startPosition;
        }
        
        instantiatedBiomes.Add(biome);
        return biome;
    }
    
    /// <summary>
    /// Clears all instantiated biomes
    /// </summary>
    [ContextMenu("Clear Biomes")]
    public void ClearBiomes()
    {
        foreach (var biome in instantiatedBiomes)
        {
            if (biome != null)
                DestroyImmediate(biome.gameObject);
        }
        instantiatedBiomes.Clear();
    }
}