using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Singleton { get; private set; }

    public Transform characterTransform;
    [SerializeField] private SpriteRenderer characterModel;
    [SerializeField] private PlayerAttack playerAttack;

    void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
        }
    }

    public void ChangeCharacter(Sprite newModel, float atkCD)
    {
        characterModel.sprite = newModel;
        playerAttack.attackCD = atkCD;
    }
}
