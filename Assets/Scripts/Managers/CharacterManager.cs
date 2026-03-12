using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Singleton { get; private set; }

    [SerializeField] private List<CharacterSO> characterList = new();
    // [SerializeField] public CharacterSO startingCharacter;
    public Transform characterTransform;
    [SerializeField] public SpriteRenderer characterModel;
    //[SerializeField] public PlayerAttack playerAttack;

    [SerializeField] private CharacterSO currentCharacter;

    [SerializeField] private CharacterSetSO[] FULL_CHARACTER_LIST; 

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // void Start()
    // {
    //     UpdateCharacterList();
    // }

    public void ChangeCharacter(Sprite newModel, float atkCD)
    {
        characterModel.sprite = newModel;
        PlayerAttack.Singleton.attackCD = atkCD;
    }

    public CharacterSO GetCurrentCharacter()
    {
        return(currentCharacter);
    }

    public void BecomeNewCharacter(CharacterSO specificCharacter = null)
    {
        if (specificCharacter != null)
        {
            if (currentCharacter)
            {
                characterList.Add(currentCharacter);
            }
            currentCharacter = specificCharacter;
            characterList.Remove(specificCharacter);
        }
        else
        {
            CharacterSO newCharacter = characterList[Random.Range(0, characterList.Count)];

            characterList.Add(currentCharacter);
            currentCharacter = newCharacter;
            characterList.Remove(newCharacter);
        }
        if (PlayerAttack.Singleton) 
            PlayerAttack.Singleton.SetCharacter(currentCharacter);
    }

    public void UpdateCharacterList()
    {
        characterList = new();

        foreach(CharacterSetSO character in FULL_CHARACTER_LIST)
        {
            if(ShopSaveSystem.GetCharacterData(character.name).isUnlocked)
            {
                characterList.Add(character.GetCurrentUpgrade().character);
            }
        }


        // for (int i = 0; i < GameManager.Singleton.characterListGM.Count; i++)
        // {
        //     characterList.Add(GameManager.Singleton.characterListGM[i]);
        // }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StarterMap")
        {
            BecomeNewCharacter(characterList[0]);
        }
    }

    public CharacterSetSO[] GetFullCharacterList()
    {
        return(FULL_CHARACTER_LIST);
    }
}
