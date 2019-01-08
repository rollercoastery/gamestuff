using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Manages the spawning and type of all foods.
*/
public class FoodManager : MonoBehaviour {

    public GameObject head;

    float currentTime;
    float randTime;
    void Awake ()
    {
        // Allocating a random float into randTime doesn't update currentTimer immediately, 
        // so the first food always spawns first, hence we initialize currentTime
        currentTime = 0.01f;
        randTime = Random.Range(10f, 30f);
    }

    GameObject food;
    Vector3 playerPos;
    Vector3 randPos;
    void Update ()
    {
        HelperFunctions.hf.Timer(ref currentTime, randTime);
        if (currentTime <= 0f)
        {
            food = ObjectManager.om.GetFood();
            if (food == null)
                return;

            playerPos = head.transform.position;
            
            // Screen space
            randPos = new Vector3(Random.Range(-1.7f, 1.7f), Random.Range(-0.93f, 0.93f), 0f);
            
            if (Vector3.Distance(playerPos, randPos) >= 0.5f && !IsAnyFoodCollide(randPos, 0.4f))
            {
                food.transform.position = randPos;
                randTime = Random.Range(10f, 30f);
            }
        }
    }

    bool IsAnyFoodCollide(Vector3 nextPos, float dist)
    {
        Vector3 currentPos;
        for (int i = 0; i < ObjectManager.om.foodAmount; ++i)
        {
            if (!ObjectManager.om.foodObjects[i].activeInHierarchy)
                continue;
                currentPos = ObjectManager.om.foodObjects[i].transform.position;
                if (Vector3.Distance(currentPos, nextPos) >= dist)
                    return false;
                else
                    return true;
        }
        return false;
    }
}