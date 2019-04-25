using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour {
    
    public int numberOfEnemiesToSpawn;  // -1 to 100. -1 for infinite
    int currentNumberOfEnemies;

    public SpawnType spawnType;
    public float spawnTimer;            // For Static spawn type
    public float fromSpawnTimer;        // For RandomDefined spawn type
    public float toSpawnTimer;          // For RandomDefined spawn type
    float randomTimer;
    float currentSpawnTimer;

    public enum SpawnType
    {
        Static,             // Spawns at a fixed time
        RandomDefined,      // Spawns at a random defined range
        Random              // Spawns at a random time
    };

    void OnEnable ()
    {
        currentNumberOfEnemies = 0;
        currentSpawnTimer = 0f;
        randomTimer = Random.Range(fromSpawnTimer, toSpawnTimer));
    }

    void Update()
    {
        // For debugging enemy spawn
        if (Input.GetKeyUp(KeyCode.N))
        {
            ObjectManager.om.GetEnemy();
        }

        switch (spawnType)
        {
            case SpawnType.Static:
                if (HelperFunctions.hf.Timer(ref currentSpawnTimer, spawnTimer))
                {
                    if (numberOfEnemiesToSpawn < 0 || currentNumberOfEnemies < numberOfEnemiesToSpawn)
                    {
                        ObjectManager.om.GetEnemy();
                        ++currentNumberOfEnemies;
                        return;
                    }
                }
                break;
            case SpawnType.RandomDefined:
                if (HelperFunctions.hf.Timer(ref currentSpawnTimer, randomTimer))
                {

                }
                break;
        }

        if (currentNumberOfEnemies >= numberOfEnemiesToSpawn)
        {
            this.gameObject.SetActive(false);
        }

    }
}
