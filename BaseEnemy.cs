using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour {

    [Header("Basic")]
	public uint health;
    public GameObject target;

    [Header("Movement")]
    public float moveSpeed;
    public float turnSpeed;
    public MovementType movementType;

    public enum MovementType
    {
        Seeking,        // Seeks target in a straight line
        Random,         // Moves randomly
        Distance,       // Keeps a small randomized range of distance from the target
        Teleport        // Teleports towards the target in steps
    }

    float randomTimer;
    float randomMaxTimer;
    Quaternion randomRotation;
    void OnEnable()
    {
        // For Random movement type
        randomMaxTimer = Random.Range(0f, 0.5f);
        randomTimer = 0f;
        randomRotation = Random.rotation;
    }

    void Update()
    {
        float p_turnSpeed = turnSpeed * GameplayData.gd.dTime;

        // Warp object to opposite screen
        Vector3 newPosition = transform.position;
        HelperFunctions.hf.WarpObject(ref newPosition, 2.88f, 1.65f);
        transform.position = newPosition;

        switch (movementType)
        {
            case MovementType.Seeking:
                transform.rotation = Quaternion.Lerp(transform.rotation, 
                                                     Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position),
                                                     p_turnSpeed);
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * GameplayData.gd.dTime);
                transform.position += transform.up * GameplayData.gd.dTime * moveSpeed;
                break;

            case MovementType.Random:
                HelperFunctions.hf.Timer(ref randomTimer, randomMaxTimer);
                if (randomTimer <= 0f)
                {
                    randomMaxTimer = Random.Range(0f, 3f);
                    randomRotation = Random.rotation;
                }

                // Rotate towards random direction
                Quaternion newRotation = Quaternion.Lerp(transform.rotation, randomRotation, p_turnSpeed);
                newRotation.x = newRotation.y = 0f;
                transform.rotation = newRotation;

                transform.position += transform.up * GameplayData.gd.dTime * moveSpeed;
                
                break;

            case MovementType.Distance:
                break;
            case MovementType.Teleport:
                break;
            default:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Insert logic for damaging the player
            float prevHealth = GameplayData.gd.health;
            GameplayData.gd.health -= 1f;
            GameplayData.gd.UpdateHealthBar(ref prevHealth);

            // try using coroutine here
            ObjectManager.om.RemoveObject(this.gameObject);
        }
    }
}
