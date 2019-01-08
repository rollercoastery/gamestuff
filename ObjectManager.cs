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

    [Header("Spawner")]
    public List<GameObject>     spawnerObjects;
    public GameObject           spawnerObject;
    public GameObject           spawnerParent;
    public int                  spawnerAmount;
    #endregion

    void Start()
    {
        //CreateObjects(ref foodObjects, ref foodObject, ref foodParent, ref foodAmount);
        //CreateObjects(ref enemyObjects, ref enemyObject, ref enemyParent, ref enemyAmount);

        // Don't use this anymore after changing the bodies to MoveTowards instead
        CreateBodies(ref bodyObjects, ref bodyObject, ref bodyParent, ref bodyAmount);
    }

    #region HELPER FUNCTIONS
    // outdated
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

    void CreateObject(ref List<GameObject> list, ref GameObject prefab, ref GameObject parent)
    {
        GameObject go = (GameObject)Instantiate(prefab);
        go.transform.parent = parent.transform;

        // Check prefab tag to set the correct default values
        switch (prefab.tag)
        {
            case "Enemy":
                go.SetActive(false);
                break;

            case "Food":
                go.SetActive(false);
                break;
                
            case "Body":
                go.GetComponent<CircleCollider2D>().enabled = false;
                go.GetComponent<Rigidbody2D>().Sleep();
                go.GetComponent<SpriteRenderer>().enabled = false;
                break;

            case "Spawner":
                break;

            default:
                return;
        }
        
        list.Add(go);
    }

    // outdated
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

    GameObject ActivateObject(ref List<GameObject> list, ref GameObject prefab, ref GameObject parent)
    {
        // If there are no objects in the list, create it
        if (list.Count <= 0)
            CreateObject(ref list, ref prefab, ref parent);

        GameObject go;
        for (int i = 0; i < list.Count; i++)
        {
            go = list[i];

            switch (go.tag)
            {
                case "Enemy":
                    if (go.activeSelf) continue;

                    go.transform.localScale = new Vector3(1f, 1f, 1f);
                    go.SetActive(true);
                    break;

                case "Food":
                    if (go.activeSelf) continue;

                    go.transform.localScale = new Vector3(1f, 1f, 1f);
                    go.SetActive(true);
                    break;

                case "Body":
                    if (go.GetComponent<SpriteRenderer>().enabled) continue;
                    
                    go.GetComponent<CircleCollider2D>().enabled = true;
                    go.GetComponent<Rigidbody2D>().WakeUp();
                    go.GetComponent<SpriteRenderer>().enabled = true;
                    go.SetActive(true);
                    break;

                case "Spawner":
                    break;

                default:
                    return null;
            }

            return go;
        }

        return null;
    }
    #endregion

    #region PUBLIC FUNCTIONS
    public void RemoveObject(GameObject obj)
    {
        StopCoroutine("DeathAnimation");
        obj.SetActive(false);
    }
    
    public GameObject GetFood()
    {
        return ActivateObject(ref foodObjects, ref foodObject, ref foodParent);
    }

    public GameObject GetBody()
    {
        return ActivateObject(ref bodyObjects, ref bodyObject, ref bodyParent);
    }

    public GameObject GetEnemy()
    {
        return ActivateObject(ref enemyObjects, ref enemyObject, ref enemyParent);
    }
    #endregion
}
