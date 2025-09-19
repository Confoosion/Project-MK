using System.Xml.Serialization;
using UnityEngine;

public class enemySO : ScriptableObject
{
    public string enemyName = "defaultName";
    public int health = 100;
    public float speed = 5.0f;
    public EnemyBehavior behavior;

    public void Attack()
    {
        behavior.OnAttack();
    }
}
