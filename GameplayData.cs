using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

/*
    For storing critical information such as gameplay information.
*/
public class GameplayData : MonoBehaviour {

    public static GameplayData gd;

    [Header("Gameplay Stats")]
    public float health;
    public uint foodCollected;
    public int bodyCount;
    public float moveSpeed;
    public float turnSpeed;

    [Header("Build Stats")]
    public float gameSpeed;
    public float dTime;
    public bool isBuildMode;
    public float slowmoFuel;
    
    // Awake happens before Start()
    void Awake ()
    {
        // Creates a singleton
        if (gd == null)
        {
            DontDestroyOnLoad(gameObject);
            gd = this;
        }
        else if (gd != this)
        {
            Destroy(gameObject);
        }

        gameSpeed = 1f;
	}

    private void Update()
    {
        dTime = Time.deltaTime * gameSpeed;
    }
}
