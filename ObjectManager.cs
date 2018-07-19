using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;

/*
    Manages the population, life and death of all objects.
*/
public class ObjectManager : MonoBehaviour {

    public static ObjectManager om;
    
    void Awake()
    {
        // Creates a singleton
        if (om == null)
        {
            DontDestroyOnLoad(gameObject);
            om = this;
        }
        else if (om != this)
        {
            Destroy(gameObject);
        }
        //HelperFunctions.hf.MakeSingleton(gameObject);
    }

    #region OBJECT VARIABLES
    [Header("Food")]
    public List<GameObject>     foodObjects;
    public GameObject           foodObject;
    public GameObject           foodParent;
    public int                  foodAmount;

    [Header("Body")]
    public List<GameObject>     bodyObjects;
    public GameObject           bodyObject;
    public GameObject           bodyParent;
    public int                  bodyAmount;

    [Header("Enemy")]
    public List<GameObject>     enemyObjects;
    public GameObject           enemyObject;
    public GameObject           enemyParent;
    public int                  enemyAmount;
    #endregion

    void Start()
    {
        CreateObjects(ref foodObjects, ref foodObject, ref foodParent, ref foodAmount);
        CreateObjects(ref enemyObjects, ref enemyObject, ref enemyParent, ref enemyAmount);
        CreateBodies(ref bodyObjects, ref bodyObject, ref bodyParent, ref bodyAmount);
    }

    #region HELPER FUNCTIONS
    void CreateObjects(ref List<GameObject> objs, ref GameObject obj, ref GameObject parent, ref int amt)
    {
        objs = new List<GameObject>();
        GameObject go;
        for (int i = 0; i < amt; i++)
        {
            go = (GameObject)Instantiate(obj);
            go.transform.parent = parent.transform;
            go.SetActive(false);
            objs.Add(go);
        }
    }

    void CreateBodies(ref List<GameObject> objs, ref GameObject obj, ref GameObject parent, ref int amt)
    {
        objs = new List<GameObject>();
        GameObject go;
        for (int i = 0; i < amt; i++)
        {
            go = (GameObject)Instantiate(obj);
            go.transform.parent = parent.transform;
            go.GetComponent<CircleCollider2D>().enabled = false;
            go.GetComponent<Rigidbody2D>().Sleep();
            go.GetComponent<SpriteRenderer>().enabled = false;
            objs.Add(go);
        }
    }

    GameObject ActivateObject(ref List<GameObject> objs)
    {
        for (int i = 0; i < objs.Count; i++)
        {
            if (!objs[i].activeInHierarchy)
                return objs[i];
        }
        return null;
    }
    #endregion

    #region PUBLIC FUNCTIONS
    public void RemoveObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public GameObject GetFood()
    {
        return ActivateObject(ref foodObjects);
    }

    public GameObject GetBody()
    {
        GameObject go = null;
        for (int i = 0; i < bodyAmount; i++)
        {
            go = bodyObjects[i];
            if (go.GetComponent<SpriteRenderer>().enabled == false)
            {
                go.GetComponent<CircleCollider2D>().enabled = true;
                go.GetComponent<Rigidbody2D>().WakeUp();
                go.GetComponent<SpriteRenderer>().enabled = true;
                break;
            }
        }

        return go;
    }

    public GameObject GetEnemy()
    {
        return ActivateObject(ref enemyObjects);
    }
    #endregion
}
