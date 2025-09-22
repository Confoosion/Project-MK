using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Characters/Laser Gun Character")]
public class LaserCharacter : CharacterSO
{
    [SerializeField] private GameObject laserObject;

    public override void UseWeapon(Transform origin)
    {

    }

    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(0);
    }
}
