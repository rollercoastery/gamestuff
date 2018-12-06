//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using System;
//using System.Runtime.Serialization.Formatters.Binary;

/*
    For storing critical information such as gameplay information.
*/
public class GameplayData : MonoBehaviour {

    public static GameplayData gd;

    [Header("Gameplay Stats")]
    public float health;
    public float maxHealth;
    public GameObject healthBar;
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
    private Slider slider;
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
        health = maxHealth;
        slider = healthBar.GetComponent<Slider>();
	}
    
    void Update()
    {
        // Game speed used for all objects that needs to be affected by it
        dTime = Time.deltaTime * gameSpeed;
    }

    public void UpdateHealthBar(ref float prevHealth)
    {
        if (prevHealth != health)
        {
            float result = health / maxHealth;
            slider.value = result;
        }
    }
}
