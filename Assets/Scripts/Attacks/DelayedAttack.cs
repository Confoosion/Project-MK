using UnityEngine;
using System.Collections;

// Simply performs a delayed attack. Does not detect hits!!!
// Requires the actual attack to be a hidden child of an active parent object
public class DelayedAttack : MonoBehaviour
{
    private float damage;
    private float atkTime;
    private float atkDelay;
    private GameObject atkObject;

    public void GetData(float dmg, float duration, float delay)
    {
        damage = dmg;
        atkTime = duration;
        atkDelay = delay;
        atkObject = gameObject.transform.GetChild(0).gameObject;
        StartCoroutine(PerformAttack());
    }

    IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(atkDelay);

        atkObject.SetActive(true);

        yield return new WaitForSeconds(atkTime);

        Destroy(this.gameObject);
    }
}
