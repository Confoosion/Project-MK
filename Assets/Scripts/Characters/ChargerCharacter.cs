using UnityEngine;

[CreateAssetMenu(menuName = "Characters/Charger Character")]
public class ChargerCharacter : CharacterSO
{
    public override void UseWeapon(Transform origin, PlayerAttack playerAttack)
    {
        float direction = (origin.localScale.x == 1) ? -1f : 1f;
        GameObject atk = Instantiate(attackObject, origin.position + new Vector3(direction * 0.5f, 0f, 0f), Quaternion.identity, origin);
        atk.GetComponent<MeleeAttack>().GetData(attackPower, attackDuration);

        // "cheated" and used PlayerAttack to access PlayerControl lol oh well it works and looks cool
        playerAttack.GetPlayerControl().ChargeMovement(attackDuration);
    }
}
