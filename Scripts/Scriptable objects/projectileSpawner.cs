using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileSpawner : MonoBehaviour
{
    // projectile that You want to spawn
    public GameObject prefabToSpawn;

    // Used to spawn multiple copies at a set interval 
    public float repeatInterval;

    //sets the direction to shoot
    public string direction;

    // Start is called before the first frame update
    void Start()
    {
        // If the object should be repeatedly spawned
        if (repeatInterval > 0)
        {
            // Call the "SpawnObject" method repeatedly
            // Wait 0.0 time before calling the first time
            // repeatInterval is how often to call the method
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }

    }

    // Used to spawn a new game object
    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            
            //sets the direction of the projectile
            prefabToSpawn.GetComponent<projectiles>().direction = direction;

            // Instantiate the prefab at the location of the current SpawnPoint object
            // Quaternion is a data structure used to represent rotations; identity = no rotation
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }

        return null;
    }
}
