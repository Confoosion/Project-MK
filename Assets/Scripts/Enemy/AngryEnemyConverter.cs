using UnityEngine;

public class AngryEnemyConverter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name.Contains("Basic"))
        {
            Debug.Log("basic enemy fell");
            Destroy(collider.gameObject);
            SpawnerManager.Singleton.CreateAngryVariant(0);
        }
        else if (collider.gameObject.name.Contains("Heavy"))
        {
            Debug.Log("Heavy enemy fell");
            Destroy(collider.gameObject);
            SpawnerManager.Singleton.CreateAngryVariant(1);

        }
    }
    
    /*
    //falls into collider...
    //deletes game object.
    //Removes from allEnemiesInWorld list
    //need to figure out what object it is... can use name of object. If name contains basic, angry basic. If name contains heavy
    //if(collider.gameObject.name.Contains("basic")){
    //      create angry basic
    //} 
        else if(collider.gameObject.name.Contains("heavy")){
            create angry heavy
        }


    void createAngryVariant(int determine){

    }

    //respawns in a spawner.. -> gets added to spawnList. Can put in specificSpawnList in spawner manager
    //update allEnemiesInWorld list

    */
}
