using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BiomeSpawnData
{
    public GameObject biomePrefab;
    [Range(0f, 100f)]
    public float spawnPercentage = 33.3f;
}

public class BiomeChainBuilder : MonoBehaviour
{
    public enum ChainMode
    {
        ProgressiveUnlock,
        WeightedRandom,
        Manual
    }
    
    [Header("Chain Mode")]
    public ChainMode chainMode = ChainMode.ProgressiveUnlock;
    
    [Header("Manual Mode - Simple List")]
    public List<GameObject> biomePrefabs = new List<GameObject>();
    
    [Header("Weighted Random Mode")]
    public List<BiomeSpawnData> weightedBiomes = new List<BiomeSpawnData>();
    public int chainLength = 6;
    [Space(10)]
    [Tooltip("Total should equal 100%")]
    [SerializeField] private float totalPercentage = 0f;
    
    public Transform biomeContainer;
    
    private List<BiomeManager> instantiatedBiomes = new List<BiomeManager>();
    
    [Header("Starting Position")]
    public Vector3 startPosition = Vector3.zero;
    
    [Header("Generation Direction")]
    public bool generateBothDirections = true;
    
    [Header("Platform Registration")]
    public bool autoRegisterPlatforms = true;
    public bool onlyRegisterFirstBiome = true;
    
    private void Start()
    {
        if (biomeContainer == null)
            biomeContainer = transform;
        
        if (chainMode == ChainMode.ProgressiveUnlock)
        {
            // Subscribe to score events
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnBiomeUnlocked.AddListener(OnBiomeUnlocked);
            }
            
            // Start with first random biome
            SpawnInitialBiome();
        }
        else if (chainMode == ChainMode.WeightedRandom)
        {
            BuildWeightedChain();
        }
        MuffinSpawner.Singleton.SpawnMuffin();
    }
    
    private void OnValidate()
    {
        // Calculate total percentage for display
        totalPercentage = 0f;
        foreach (var biomeData in weightedBiomes)
        {
            totalPercentage += biomeData.spawnPercentage;
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
        List<GameObject> availableBiomes = chainMode == ChainMode.ProgressiveUnlock ? biomePrefabs : GetBiomePrefabsList();
        
        if (availableBiomes.Count == 0)
        {
            Debug.LogWarning("No biome prefabs assigned!");
            return;
        }
        
        // Pick random biome for first one
        int randomIndex = Random.Range(0, availableBiomes.Count);
        GameObject randomBiome = availableBiomes[randomIndex];
        
        GameObject biomeObj = Instantiate(randomBiome, biomeContainer);
        BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
        
        if (biome != null)
        {
            biome.transform.position = startPosition;
            instantiatedBiomes.Add(biome);
            
            // Always register the first biome's platforms
            if (autoRegisterPlatforms)
            {
                biome.RegisterPlatforms();
            }
            
            Debug.Log($"Spawned initial biome: {randomBiome.name}");
        }
    }
    
    private List<GameObject> GetBiomePrefabsList()
    {
        List<GameObject> prefabs = new List<GameObject>();
        foreach (var biomeData in weightedBiomes)
        {
            if (biomeData.biomePrefab != null)
                prefabs.Add(biomeData.biomePrefab);
        }
        return prefabs;
    }
    
    /// <summary>
    /// Builds a chain using weighted random selection
    /// </summary>
    [ContextMenu("Build Weighted Chain")]
    public void BuildWeightedChain()
    {
        ClearBiomes();
        
        if (weightedBiomes.Count == 0)
        {
            Debug.LogWarning("No weighted biomes assigned!");
            return;
        }
        
        // Validate percentages
        float total = 0f;
        foreach (var biomeData in weightedBiomes)
        {
            total += biomeData.spawnPercentage;
        }
        
        if (Mathf.Abs(total - 100f) > 0.1f)
        {
            Debug.LogWarning($"Spawn percentages total {total}% instead of 100%. Normalizing...");
        }
        
        if (generateBothDirections)
        {
            BuildBidirectionalChain();
        }
        else
        {
            BuildUnidirectionalChain();
        }
    }
    
    /// <summary>
    /// Builds chain in both left and right directions
    /// </summary>
    private void BuildBidirectionalChain()
    {
        // Spawn center biome first
        GameObject centerBiomePrefab = SelectWeightedBiome();
        GameObject centerBiomeObj = Instantiate(centerBiomePrefab, biomeContainer);
        BiomeManager centerBiome = centerBiomeObj.GetComponent<BiomeManager>();
        
        if (centerBiome == null)
        {
            Debug.LogError($"Center biome prefab is missing BiomeManager component!");
            Destroy(centerBiomeObj);
            return;
        }
        
        centerBiome.transform.position = startPosition;
        instantiatedBiomes.Add(centerBiome);
        
        // Register platforms for center biome (it's the first biome)
        if (autoRegisterPlatforms)
        {
            centerBiome.RegisterPlatforms();
        }
        
        // Calculate how many biomes to spawn on each side
        int biomesToSpawn = chainLength - 1; // Minus the center biome
        int biomesRight = biomesToSpawn / 2;
        int biomesLeft = biomesToSpawn - biomesRight;
        
        // Build right side
        BiomeManager currentBiome = centerBiome;
        for (int i = 0; i < biomesRight; i++)
        {
            GameObject selectedBiome = SelectWeightedBiome();
            GameObject biomeObj = Instantiate(selectedBiome, biomeContainer);
            BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
            
            if (biome != null)
            {
                currentBiome.ConnectBiomeToRight(biome);
                instantiatedBiomes.Add(biome);
                currentBiome = biome;
                
                // Only register if not limiting to first biome only
                if (autoRegisterPlatforms && !onlyRegisterFirstBiome)
                {
                    biome.RegisterPlatforms();
                }
            }
            else
            {
                Destroy(biomeObj);
            }
        }
        
        // Build left side
        currentBiome = centerBiome;
        for (int i = 0; i < biomesLeft; i++)
        {
            GameObject selectedBiome = SelectWeightedBiome();
            GameObject biomeObj = Instantiate(selectedBiome, biomeContainer);
            BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
            
            if (biome != null)
            {
                currentBiome.ConnectBiomeToLeft(biome);
                instantiatedBiomes.Insert(0, biome); // Insert at beginning to maintain order
                currentBiome = biome;
                
                // Only register if not limiting to first biome only
                if (autoRegisterPlatforms && !onlyRegisterFirstBiome)
                {
                    biome.RegisterPlatforms();
                }
            }
            else
            {
                Destroy(biomeObj);
            }
        }
        
        Debug.Log($"Built bidirectional chain: {biomesLeft} left + 1 center + {biomesRight} right = {instantiatedBiomes.Count} total biomes");
    }
    
    /// <summary>
    /// Builds chain only to the right
    /// </summary>
    private void BuildUnidirectionalChain()
    {
        BiomeManager previousBiome = null;
        
        for (int i = 0; i < chainLength; i++)
        {
            GameObject selectedBiome = SelectWeightedBiome();
            
            if (selectedBiome == null)
            {
                Debug.LogError("Failed to select biome!");
                continue;
            }
            
            GameObject biomeObj = Instantiate(selectedBiome, biomeContainer);
            BiomeManager biome = biomeObj.GetComponent<BiomeManager>();
            
            if (biome == null)
            {
                Debug.LogError($"Biome prefab {selectedBiome.name} is missing BiomeManager component!");
                Destroy(biomeObj);
                continue;
            }
            
            instantiatedBiomes.Add(biome);
            
            // First biome goes at start position
            if (i == 0)
            {
                biome.transform.position = startPosition;
                
                // Always register first biome's platforms
                if (autoRegisterPlatforms)
                {
                    biome.RegisterPlatforms();
                }
            }
            else if (previousBiome != null)
            {
                // Connect to previous biome
                previousBiome.ConnectBiomeToRight(biome);
                
                // Only register if not limiting to first biome only
                if (autoRegisterPlatforms && !onlyRegisterFirstBiome)
                {
                    biome.RegisterPlatforms();
                }
            }
            
            previousBiome = biome;
        }
        
        Debug.Log($"Built unidirectional chain of {instantiatedBiomes.Count} biomes");
    }
    
    /// <summary>
    /// Selects a biome based on weighted percentages
    /// </summary>
    private GameObject SelectWeightedBiome()
    {
        float totalWeight = 0f;
        foreach (var biomeData in weightedBiomes)
        {
            totalWeight += biomeData.spawnPercentage;
        }
        
        float randomValue = Random.Range(0f, totalWeight);
        float cumulativeWeight = 0f;
        
        foreach (var biomeData in weightedBiomes)
        {
            cumulativeWeight += biomeData.spawnPercentage;
            if (randomValue <= cumulativeWeight)
            {
                return biomeData.biomePrefab;
            }
        }
        
        // Fallback to last biome
        return weightedBiomes[weightedBiomes.Count - 1].biomePrefab;
    }
    
    private void OnBiomeUnlocked(int biomeIndex)
    {
        // Spawn next random biome (for progressive unlock mode)
        List<GameObject> availableBiomes = biomePrefabs.Count > 0 ? biomePrefabs : GetBiomePrefabsList();
        
        if (availableBiomes.Count == 0) return;
        
        int randomIndex = Random.Range(0, availableBiomes.Count);
        BiomeManager newBiome = AddBiomeToChain(availableBiomes[randomIndex]);
        
        // Register the newly unlocked biome's platforms
        if (newBiome != null && autoRegisterPlatforms)
        {
            newBiome.RegisterPlatforms();
            Debug.Log($"Biome {biomeIndex} unlocked! Platforms registered.");
        }
    }
    
    /// <summary>
    /// Builds a chain of biomes from the manual prefab list
    /// </summary>
    [ContextMenu("Build Manual Chain")]
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
                
                // Always register first biome's platforms
                if (autoRegisterPlatforms)
                {
                    biome.RegisterPlatforms();
                }
            }
            else if (previousBiome != null)
            {
                // Connect to previous biome
                previousBiome.ConnectBiomeToRight(biome);
                
                // Only register if not limiting to first biome only
                if (autoRegisterPlatforms && !onlyRegisterFirstBiome)
                {
                    biome.RegisterPlatforms();
                }
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
        
        // Note: Platform registration is handled by the caller (OnBiomeUnlocked)
        // This allows more control over when platforms are registered
        
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