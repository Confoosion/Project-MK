using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int heatlh;
    public float speed;
    //public float attackSpeed;
    public bool isFlying;

    public Sprite enemyModel;

    
}
