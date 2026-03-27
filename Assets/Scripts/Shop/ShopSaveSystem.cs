using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PerkData
{
    public string perkName;
    public bool isUnlocked;
    
    public PerkData(string name, bool unlocked)
    {
        perkName = name;
        isUnlocked = unlocked;
    }
}

[Serializable]
public class PerkMachineData
{
    public int currentTier;
    public List<PerkData> ownedPerks = new List<PerkData>();
    public int pityCounter = 0;

    public PerkMachineData(int tier)
    {
        currentTier = tier;
    }
}

[Serializable]
public class CharacterSetData
{
    public string characterSetName;  // SO name as unique ID
    public int currentLevel;
    public bool isUnlocked;
    
    public CharacterSetData(string name, int level, bool unlocked)
    {
        characterSetName = name;
        currentLevel = level;
        isUnlocked = unlocked;
    }
}

[Serializable]
public class ShopSaveData
{
    public List<CharacterSetData> characterSets = new List<CharacterSetData>();
    public int characterCurrency;

    public PerkMachineData perkMachine = new PerkMachineData(1);
}

public static class ShopSaveSystem
{
    private static string SavePath => Path.Combine(Application.persistentDataPath, "shopData.json");
    
    // Runtime data (NOT the ScriptableObjects!)
    private static Dictionary<string, CharacterSetData> runtimeCharacterData = new Dictionary<string, CharacterSetData>();
    private static int runtimeCurrency = 0;
    private static PerkMachineData runtimePerkMachineData = new PerkMachineData(1);
    private static CharacterSetSO[] runtimeCharacterSets;
    
    // Reset on domain reload (when Unity recompiles scripts)
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    // private static void OnDomainReload()
    // {
    //     runtimeCharacterData.Clear();
    //     runtimeCurrency = 0;
    //     runtimePerkMachineData = new PerkMachineData(1);
    //     runtimeCharacterSets = null;
    //     Debug.Log("ShopSaveSystem: Reset after domain reload");
    // }
    
    // ========== SAVE DATA ==========
    public static void Save(CharacterSetSO[] characterSets, int currency)
    {
        ShopSaveData saveData = new ShopSaveData();
        saveData.characterCurrency = currency;
        
        // Save character sets
        foreach (var charSet in characterSets)
        {
            string setName = charSet.name;
            
            if (runtimeCharacterData.ContainsKey(setName))
            {
                saveData.characterSets.Add(runtimeCharacterData[setName]);
            }
        }
        
        // Save perk machine
        saveData.perkMachine = runtimePerkMachineData;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(SavePath, json);
        
        Debug.Log($"Shop data saved to: {SavePath}");
    }

    public static void Save()
    {
        if(runtimeCharacterSets == null)
        {
            Debug.LogError("Save Unsuccessful. References not loaded. Call Load() first");
            return;
        }

        Save(runtimeCharacterSets, runtimeCurrency);
    }
    
    // ========== LOAD DATA ==========
    public static void Load(CharacterSetSO[] characterSets, out int loadedCurrency)
    {
        runtimeCharacterSets = characterSets;

        // Initialize runtime data from ScriptableObject defaults
        runtimeCharacterData.Clear();
        foreach (var charSet in characterSets)
        {
            runtimeCharacterData[charSet.name] = new CharacterSetData(
                charSet.name,
                1,      // Default level 1
                false   // Default locked
            );
        }
        
        runtimePerkMachineData = new PerkMachineData(1);

        // Default currency
        loadedCurrency = 0;
        
        // If save file exists, load from it
        if (File.Exists(SavePath))
        {
            try
            {
                string json = File.ReadAllText(SavePath);
                ShopSaveData saveData = JsonUtility.FromJson<ShopSaveData>(json);
                
                // Load character data
                foreach (var savedChar in saveData.characterSets)
                {
                    if (runtimeCharacterData.ContainsKey(savedChar.characterSetName))
                    {
                        runtimeCharacterData[savedChar.characterSetName] = savedChar;
                    }
                }
                
                // Load perk machine
                if(saveData.perkMachine != null)
                {
                    runtimePerkMachineData = saveData.perkMachine;
                }

                // Load currency
                loadedCurrency = saveData.characterCurrency;
                runtimeCurrency = loadedCurrency;
                
                Debug.Log("Shop data loaded successfully!");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load shop data: {e.Message}");
            }
        }
        else
        {
            Debug.Log("No save file found. Using default values.");
            
            // First 3 characters unlocked by default
            if (characterSets.Length > 2)
            {
                string unlockedName;
                for(int index = 0; index < 3; index++)
                {
                    unlockedName = characterSets[index].name;
                    runtimeCharacterData[unlockedName].isUnlocked = true;    
                }
            }
        }
    }
    
    // ========== GET/UPDATE CHARACTER DATA ==========
    public static CharacterSetData GetCharacterData(string characterSetName)
    {
        return runtimeCharacterData.ContainsKey(characterSetName) 
            ? runtimeCharacterData[characterSetName] 
            : null;
    }
    
    public static void UpdateCharacterLevel(string characterSetName, int newLevel)
    {
        if (runtimeCharacterData.ContainsKey(characterSetName))
        {
            runtimeCharacterData[characterSetName].currentLevel = newLevel;
        }
    }
    
    public static void UnlockCharacter(string characterSetName)
    {
        if (runtimeCharacterData.ContainsKey(characterSetName))
        {
            runtimeCharacterData[characterSetName].isUnlocked = true;

            Save();
        }
    }

    public static bool IsCharacterUnlocked(string characterSetName)
    {
        return(runtimeCharacterData[characterSetName].isUnlocked);
    }

    // ========== PERK MACHINE DATA ==========
    public static bool IsPerkUnlocked(string perkName)
    {
        PerkMachineData data = GetPerkMachineData();
        PerkData perkData = data.ownedPerks.Find(p => p.perkName == perkName);
        return(perkData != null && perkData.isUnlocked);    
    }

    public static void UnlockPerk(string perkName)
    {
        PerkMachineData data = GetPerkMachineData();
        PerkData existingPerk = data.ownedPerks.Find(p => p.perkName == perkName);

        if(existingPerk != null)
            existingPerk.isUnlocked = true;
        else
            data.ownedPerks.Add(new PerkData(perkName, true));
    }

    public static List<PerkData> GetUnlockedPerks()
    {
        PerkMachineData data = GetPerkMachineData();
        return(data.ownedPerks.FindAll(p => p.isUnlocked));
    }

    public static PerkMachineData GetPerkMachineData()
    {
        return(runtimePerkMachineData);
    }
    
    public static void UpgradePerkMachineTier(int newTier)
    {
        runtimePerkMachineData.currentTier = newTier;
    }

    // ========== CURRENCY ==========
    public static int GetCurrency()
    {
        return runtimeCurrency;
    }
    
    public static void SetCurrency(int amount)
    {
        runtimeCurrency = amount;
    }

    public static void AddCurrency(int amount)
    {
        runtimeCurrency += amount;
        Save();
    }
    
    // ========== UTILITY ==========
    public static void DeleteSaveData()
    {
        if (File.Exists(SavePath))
        {
            File.Delete(SavePath);
            Debug.Log("Save file deleted.");
        }
    }
    
    public static bool SaveExists()
    {
        return File.Exists(SavePath);
    }
}