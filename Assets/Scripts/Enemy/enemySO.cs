using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    public int heatlh;
    public float speed;
    public EnemyAttackType attack;
    public bool flying;
    public List<EnemySO> multiple;

    public Sprite enemyModel;

    
}

public enum EnemyAttackType
{
    normal,
    projectile,
    bomb,
    shield
}
