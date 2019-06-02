using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour {
    
    public int numberOfEnemiesToSpawn;  // -1 to 100. -1 for infinite
    int currentNumberOfEnemies;         // Current number of enemies that have been spawned by this spawner

    public SpawnType spawnType;         // Storing the current spawn type enum
    public float spawnTimer;            // Static spawn type, time before 1 enemy is spawned
    public float fromSpawnTimer;        // RandomRange spawn type, random range from this value
    public float toSpawnTimer;          // RandomRange spawn type, random range to this value
    public int batchOfEnemies;          // Beats spawn type, how many enemies to spawn one after the other before delayTimer kicks in
    public float delayTimer;            // Beats spawn type, pause duration until spawn resumes
    float randomRangeTimer;             // RandomRange spawn type, max random timer
    float currentTimer;                 // The current timer for all spawn types

    public enum SpawnType
    {
        Static,             // Spawns at a fixed time
        RandomRange,        // Spawns at a random defined range
        Beats               // Spawns a defined number of enemies after a defined delayed time has passed
    };

    void OnEnable ()
    {
        currentNumberOfEnemies = 0;
        currentTimer = 0f;
        randomRangeTimer = Random.Range(fromSpawnTimer, toSpawnTimer);
    }

    void Update()
    {
        // For debugging enemy spawn
        if (Input.GetKeyUp(KeyCode.N))
        {
            ObjectManager.om.GetEnemy(new Vector3(0f,0f,0f));
        }

        switch (spawnType)
        {
            case SpawnType.Static:
                if (CheckEnemyCount() && 
                    HelperFunctions.hf.Timer(ref currentTimer, spawnTimer))
                {
                    ObjectManager.om.GetEnemy(transform.position);
                    ++currentNumberOfEnemies;
                    return;
                }
                break;
            case SpawnType.RandomRange:
                if (CheckEnemyCount() &&
                    HelperFunctions.hf.Timer(ref currentTimer, randomRangeTimer))
                {
                    randomRangeTimer = Random.Range(fromSpawnTimer, toSpawnTimer);
                    ObjectManager.om.GetEnemy(transform.position);
                    ++currentNumberOfEnemies;
                }
                break;
            case SpawnType.Beats:
                if (CheckEnemyCount() && 
                    HelperFunctions.hf.Timer(ref currentTimer, 1f))
                {
                     
                }
                break;
        }

        if (currentNumberOfEnemies > numberOfEnemiesToSpawn)
        {
            this.gameObject.SetActive(false);
        }

    }

    // Check the current number of enemies that has been spawned by this spawner
    bool CheckEnemyCount()
    {
        return (numberOfEnemiesToSpawn < 0 || currentNumberOfEnemies < numberOfEnemiesToSpawn) ? true : false;
    }
}
