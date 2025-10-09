using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Singleton { get; private set; }

    [SerializeField] private List<CharacterSO> characterList = new();
    [SerializeField] private CharacterSO startingCharacter;
    public Transform characterTransform;
    [SerializeField] private SpriteRenderer characterModel;
    [SerializeField] private PlayerAttack playerAttack;

    [SerializeField] private CharacterSO currentCharacter;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    void Start()
    {
        if (startingCharacter != null)
        {
            BecomeNewCharacter(startingCharacter);
        }
    }

    public void ChangeCharacter(Sprite newModel, float atkCD)
    {
        characterModel.sprite = newModel;
        playerAttack.attackCD = atkCD;
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

        playerAttack.SetCharacter(currentCharacter);
    }
}
