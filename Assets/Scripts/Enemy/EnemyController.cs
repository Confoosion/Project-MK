using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemySO enemyType;

    private float health;

    private void Start()
    {
        health = enemyType.health;
    }
    void Update()
    {
           
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player should be dead");
            //Player should be dead
        }
    }

    //ENEMY Taking Damage and dying

    public void enemyTakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy took damage, health is now: " + health);
        if (health <= 0)
        {
            enemyDeath();
        }
    }


    private void enemyDeath()
    {
        
        Destroy(this.gameObject);
    }


  
}
