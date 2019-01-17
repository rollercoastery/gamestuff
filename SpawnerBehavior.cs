using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehavior : MonoBehaviour {

    void Start ()
    {
		
    }
    
    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.N))
        {
            ObjectManager.om.GetEnemy();
        }
        
    }
}
