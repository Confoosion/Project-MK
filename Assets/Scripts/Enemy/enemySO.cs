using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public float health;
    public float speed;
    //public float attackSpeed;
    public bool isFlying;

    public Sprite enemyModel;

    
}
