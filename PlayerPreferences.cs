using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    For storing non-critical information such as player preferences in the game options menu.
*/
public class PlayerPreferences : MonoBehaviour {

    public static PlayerPreferences playerPref;
    public float musicVolume;


    // Awake happens before Start()
    void Awake()
    {
        // Creates a singleton
        if (playerPref == null)
        {
            DontDestroyOnLoad(gameObject);
            playerPref = this;
        }
        else if (playerPref != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 100f);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
