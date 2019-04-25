using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyBehavior : MonoBehaviour {

    public GameplayData gd;

    public GameObject front;

    GameObject head; 

    void Start ()
    {
        //bodyCount = gd.bodyCount;
        head = ObjectManager.om.transform.GetChild(0).gameObject;
	}

    float currentTimer;
	void Update ()
    {
        GameObject go;
        Transform followObj;
        for (int i = 0; i < ObjectManager.om.bodyAmount; i++)
        {
            go = ObjectManager.om.bodyList[i];
            
            if (i == 0)
            {
                // ObjectManager -> Head (0) -> Head.Spawner (0)
                followObj = head.transform.GetChild(0).transform;
            }
            else
            {
                // ObjectManager -> Body (i-1) -> Body.Spawner (0)
                followObj = ObjectManager.om.bodyList[i - 1].transform.GetChild(0).transform;
            }
            go.transform.position = followObj.position;
            go.transform.rotation = Quaternion.Lerp(go.transform.rotation, followObj.rotation, Time.deltaTime * gd.gameSpeed);
        }
    }
        
}
