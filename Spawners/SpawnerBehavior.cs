using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour {
    
    public int numberOfEnemiesToSpawn;  // -1 to 100. -1 for infinite
    int currentNumberOfEnemies;         // Current number of enemies that have been spawned by this spawner

    public SpawnType spawnType;         // Storing the current spawn type enum
    public float spawnTimer;            // Static & Beats spawn type, time before 1 enemy is spawned
    public float fromSpawnTimer_randr;  // RandomRange spawn type, random range from this value
    public float toSpawnTimer_randr;    // RandomRange spawn type, random range to this value
    public int batchOfEnemies_beats;    // Beats spawn type, how many enemies to spawn one after the other before delayTimer kicks in
    public float delayTimer_beats;      // Beats spawn type, pause duration until spawn resumes
    uint numberOfEnemiesSpawned_beats;  // Beats spawn type, track the number of enemies spawned per batch
    float currentSecondTimer_beats;     // Beats spawn type, current secondary timer for delayTimer_beats
    float randomRangeTimer_rand;        // RandomRange spawn type, max random timer
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
        numberOfEnemiesSpawned_beats = 0;
        currentTimer = currentSecondTimer_beats = 0f;
        randomRangeTimer_rand = Random.Range(fromSpawnTimer_randr, toSpawnTimer_randr);
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
                if (IsSpawnEnemySuccess(spawnTimer))
                    SpawnEnemy();
                break;
            case SpawnType.RandomRange:
                if (IsSpawnEnemySuccess(randomRangeTimer_rand))
                {
                    randomRangeTimer_rand = Random.Range(fromSpawnTimer_randr, toSpawnTimer_randr);
                    SpawnEnemy();
                }
                break;
            case SpawnType.Beats:

                if (IsSpawnEnemySuccess(spawnTimer))
                {
                    if (numberOfEnemiesSpawned_beats < batchOfEnemies_beats)
                    {
                        SpawnEnemy();
                        ++numberOfEnemiesSpawned_beats;
                        print("spawned");
                    }
                    else if (numberOfEnemiesSpawned_beats >= batchOfEnemies_beats)
                    {
                        numberOfEnemiesSpawned_beats = 0;
                        currentTimer = currentSecondTimer_beats = 0f;
                        print("reset");
                    }
                }
                break;
        }

        if (currentNumberOfEnemies > numberOfEnemiesToSpawn)
        {
            this.gameObject.SetActive(false);
        }

    }

    void SpawnEnemy()
    {
        ObjectManager.om.GetEnemy(transform.position);
        ++currentNumberOfEnemies;
    }

    // 
    bool IsSpawnEnemySuccess(float timer)
    {
        if (CheckEnemyCount() &&
            HelperFunctions.hf.Timer(ref currentTimer, timer))
        {
            return true;
        }
        return false;
    }

    // Check the current number of enemies that has been spawned by this spawner
    // return true when 
    bool CheckEnemyCount()
    {
        return (numberOfEnemiesToSpawn < 0 || currentNumberOfEnemies < numberOfEnemiesToSpawn) ? true : false;
    }
}
