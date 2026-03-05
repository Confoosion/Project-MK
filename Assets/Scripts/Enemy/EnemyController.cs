using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public EnemySO enemyType;

    protected float speed;
    private float health;

    private void Start()
    {
        health = enemyType.health;
        SpawnerManager.Singleton.allEnemiesInWorld.Add(this.gameObject);
    }

    protected void SetSpeed()
    {
        // Set speed (also checks for SlowEnemies perk)
        PerkSO slowPerk = PerksManager.Singleton.GetActivePerk();
        if(slowPerk != null && slowPerk.perkType == PerkType.SlowEnemies)
            speed = enemyType.speed - slowPerk.value;
        else
            speed = enemyType.speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            //Player should be dead
            collision.gameObject.GetComponent<PlayerControl>().playerDeath();
        }
    }

    //ENEMY Taking Damage and dying

    public void enemyTakeDamage(float damage)
    {
        float extraDamage = 0f;

        // Check for damage perk
        PerkSO dmgPerk = PerksManager.Singleton.GetActivePerk();
        if(dmgPerk != null && dmgPerk.perkType == PerkType.Damage)
            extraDamage += dmgPerk.value;

        health -= (damage + extraDamage);

        if (health <= 0)
        {
            enemyDeath();
            if (this.gameObject.name.Contains("AngryBasic"))
            {
                GameManager.Singleton.addAngryBasicEnemyKill();
            }
            else if (this.gameObject.name.Contains("AngryHeavy"))
            {
                GameManager.Singleton.addAngryHeavyEnemyKill();
            }
            else if (this.gameObject.name.Contains("Basic"))
            {
                GameManager.Singleton.addBasicEnemyKill();
            }
            else if (this.gameObject.name.Contains("Heavy"))
            {
                GameManager.Singleton.addHeavyEnemyKill();
            }
        }
    }


    private void enemyDeath()
    {

        Destroy(this.gameObject);
    }



}
