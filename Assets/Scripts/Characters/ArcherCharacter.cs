using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Archer Character")]
public class ArcherCharacter : CharacterSO
{
    [SerializeField] private GameObject arrow;

    public override void UseWeapon(Transform origin)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(arrow, origin.position + new Vector3(0f, 0.25f, 0f), Quaternion.Euler(new Vector3(0f, 0f, (direction == -1) ? 180f : 0f)));

        atk.GetComponent<RangeAttack>().GetData(attackPower);
    } 
}
