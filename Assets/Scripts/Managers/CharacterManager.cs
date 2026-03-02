using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Singleton { get; private set; }

    private List<CharacterSO> characterList = new();
    [SerializeField] public CharacterSO startingCharacter;
    public Transform characterTransform;
    [SerializeField] public SpriteRenderer characterModel;
    //[SerializeField] public PlayerAttack playerAttack;

    [SerializeField] private CharacterSO currentCharacter;

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

    void Start()
    {
        UpdateCharacterList();
    }

    public void ChangeCharacter(Sprite newModel, float atkCD)
    {
        Debug.Log($"before: {characterModel.gameObject.name} | {characterModel.sprite}");
        characterModel.sprite = newModel;
        Debug.Log($"after: {characterModel.gameObject.name} | {characterModel.sprite}");
        PlayerAttack.Singleton.attackCD = atkCD;
    }

    public void BecomeNewCharacter(CharacterSO specificCharacter = null)
    {
        if (specificCharacter != null)
        {
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
        while (characterList.Count > 0)
        {
            characterList.RemoveAt(0);
        }

        for (int i = 0; i < GameManager.Singleton.characterListGM.Count; i++)
        {
            characterList.Add(GameManager.Singleton.characterListGM[i]);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "MainMenu")
        {
            if (startingCharacter != null)
            {
                BecomeNewCharacter(startingCharacter);
            }
        }
    }
}
