using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    [Header("Score Settings")]
    [SerializeField] private int currentScore = 0;
    [SerializeField] private int scorePerBiome = 10;
    
    [Header("Events")]
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnBiomeUnlocked; // Passes biome index
    
    private int biomesUnlocked = 1; // Start with first biome
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddScore(int amount)
    {
        currentScore += amount;
        OnScoreChanged?.Invoke(currentScore);
        
        CheckForBiomeUnlock();
    }
    
    private void CheckForBiomeUnlock()
    {
        int biomesEarned = (currentScore / scorePerBiome) + 1;
        
        if (biomesEarned > biomesUnlocked)
        {
            biomesUnlocked = biomesEarned;
            OnBiomeUnlocked?.Invoke(biomesUnlocked - 1); // Pass the new biome index
            Debug.Log($"New biome unlocked! Total biomes: {biomesUnlocked}");
        }
    }
    
    public int GetCurrentScore()
    {
        return currentScore;
    }
    
    public int GetBiomesUnlocked()
    {
        return biomesUnlocked;
    }
    
    public int GetScoreForNextBiome()
    {
        return (biomesUnlocked * scorePerBiome) - currentScore;
    }
    
    public void ResetScore()
    {
        currentScore = 0;
        biomesUnlocked = 1;
        OnScoreChanged?.Invoke(currentScore);
    }
}