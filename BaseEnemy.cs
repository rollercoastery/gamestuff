using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    [Header("Basic")]
	public uint health;
    public GameObject target;   // Target to seek

    [Header("Movement")]
    public float spawnedTimer;          // Set duration after spawning to count down from
    public float moveSpeed;
    public float turnSpeed;
    public MovementType movementType;   // Movement type that will eventually be set after spawning period

    public enum MovementType
    {
        Straight,       // Moves in a straight line when just spawned
        Seeking,        // Seeks target in a straight line
        Random,         // Moves randomly
        Distance,       // Keeps a small randomized range of distance from the target
        Teleport        // Teleports towards the target in steps
    }

    float randRotationTimer;
    float randRotationMaxTimer;
    Quaternion randomRotation;

    MovementType currentMovementType;
    float currentSpawnedTimer;
    bool isCollided;    // For dying animation

    void OnEnable()
    {
        // For Random movement type
        randRotationMaxTimer = Random.Range(0f, 0.5f);
        randRotationTimer = 0f;
        randomRotation = Random.rotation;

        currentMovementType = MovementType.Straight;
        isCollided = false;
    }
    
    void Update()
    {
        float p_turnSpeed = turnSpeed * GameplayData.gd.dTime;

        if (StartSpawnedTimer(spawnedTimer))
            currentMovementType = movementType;

        switch (currentMovementType)
        {
            case MovementType.Straight:
                MoveStraight();
                break;

            case MovementType.Seeking:
                WarpObject();
                transform.rotation = Quaternion.Lerp(transform.rotation, 
                                                     Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position),
                                                     p_turnSpeed);
                MoveStraight();
                break;

            case MovementType.Random:
                WarpObject();

                // Get a random timer
                HelperFunctions.hf.Timer(ref randRotationTimer, randRotationMaxTimer);
                if (randRotationTimer <= 0f)
                {
                    randRotationMaxTimer = Random.Range(0f, 3f);
                    randomRotation = Random.rotation;
                }

                // Rotate towards random direction
                Quaternion newRotation = Quaternion.Lerp(transform.rotation, randomRotation, p_turnSpeed);
                newRotation.x = newRotation.y = 0f;
                transform.rotation = newRotation;

                MoveStraight();
                break;

            case MovementType.Distance:
                break;
            case MovementType.Teleport:
                break;
            default:
                break;
        }

        if (isCollided)
        {
            StartCoroutine("DeathAnimation");
            if (transform.localScale.x <= .1f)
                ObjectManager.om.RemoveObject(this.gameObject);
        }
    }

    // Kick start the spawned timer and sets movement to the chosen one after
    private bool StartSpawnedTimer(float timer)
    {
        HelperFunctions.hf.Timer(ref currentSpawnedTimer, timer);
        return currentSpawnedTimer < timer ? false : true;
    }

    private void MoveStraight()
    {
        //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * GameplayData.gd.dTime);
        transform.position += transform.up * GameplayData.gd.dTime * moveSpeed;
    }

    // Warp object to opposite screen
    private void WarpObject()
    {
        Vector3 newPosition = transform.position;
        HelperFunctions.hf.WarpObject(ref newPosition, 2.88f, 1.65f);
        transform.position = newPosition;
    }

    IEnumerator DeathAnimation()
    {
        transform.localScale = HelperFunctions.hf.Scaler(transform.localScale, Vector3.zero, 5f);
        yield return new WaitForSeconds(1f);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Insert logic for damaging the player
            float prevHealth = GameplayData.gd.health;
            GameplayData.gd.health -= 1f;
            GameplayData.gd.UpdateHealthBar(ref prevHealth);
            isCollided = true;
        }
    }
}
