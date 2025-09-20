using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Sword Character")]
public class SwordCharacter : CharacterSO
{
    [SerializeField] private GameObject hitbox;

    public override void UseWeapon(Transform origin)
    {
        Instantiate(hitbox, origin.position + new Vector3(-1f, 0f, 0f), Quaternion.identity, origin);
    }
}
