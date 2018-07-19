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
    bool isRandomMove;
    Quaternion randomRotation;
    void OnEnable()
    {
        // For Random movement type
        randomMaxTimer = Random.Range(0f, 0.5f);
        isRandomMove = false;
        randomRotation = Random.rotation;
    }

    void Update()
    {
        
        switch (movementType)
        {
            case MovementType.Seeking:
                transform.rotation = Quaternion.Lerp(transform.rotation, 
                                                     Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position), 
                                                     turnSpeed * GameplayData.gd.dTime);
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, moveSpeed * GameplayData.gd.dTime);
                transform.position += transform.up * GameplayData.gd.dTime * moveSpeed;
                break;
            case MovementType.Random:
                HelperFunctions.hf.Timer(ref randomTimer, randomMaxTimer);
                if (randomTimer <= 0f)
                {
                    randomMaxTimer = Random.Range(0f, 3f);
                    randomRotation = Random.rotation;
                    isRandomMove = true;
                }

                if (isRandomMove)
                {
                    Quaternion newRotation = Quaternion.Lerp(transform.rotation, randomRotation, turnSpeed * GameplayData.gd.dTime);
                    newRotation.x = newRotation.y = 0f;
                    transform.rotation = newRotation;

                    transform.position += transform.up * GameplayData.gd.dTime * moveSpeed;
                }

                if (transform.rotation.y == randomRotation.y)
                    isRandomMove = false;
                break;
            case MovementType.Distance:
                break;
            case MovementType.Teleport:
                break;
            default:
                break;
        }
    }
}
