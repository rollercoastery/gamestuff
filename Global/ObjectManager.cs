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

        cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        head = GameObject.FindWithTag("Player");
        //HelperFunctions.hf.MakeSingleton(gameObject);
    }

    [Header("Global Objects")]
    public GameplayData gd;
    public Camera cam;
    public GameObject head;

    #region OBJECT VARIABLES
    [Header("Food")]
    public List<GameObject>     foodList;
    public GameObject           foodPrefab;
    public GameObject           foodContainer;

    [Header("Body")]
    public List<GameObject>     bodyList;
    public GameObject           bodyPrefab;
    public GameObject           bodyContainer;
    public int                  bodyAmount;

    [Header("Enemy")]
    public List<GameObject>     enemyList;
    public GameObject           enemyPrefab;
    public GameObject           enemyContainer;

    [Header("Spawner")]
    public List<GameObject>     spawnerList;
    public GameObject           spawnerPrefab;
    public GameObject           spawnerContainer;
    #endregion

    void Start()
    {
        for (int i = 0; i < bodyAmount; i++)
            CreateObject(ref bodyList, ref bodyPrefab, ref bodyContainer);
    }

    #region HELPER FUNCTIONS
    GameObject CreateObject(ref List<GameObject> list, ref GameObject prefab, ref GameObject container)
    {
        GameObject go = (GameObject)Instantiate(prefab);
        go.transform.parent = container.transform;

        // Check prefab tag to set the correct default values
        switch (prefab.tag)
        {
            case "Enemy":
            case "Food":
                go.SetActive(true);
                break;
                
            case "Body":
                go.GetComponent<CircleCollider2D>().enabled = false;
                go.GetComponent<Rigidbody2D>().Sleep();
                go.GetComponent<SpriteRenderer>().enabled = false;
                break;

            case "Spawner":
                break;

            default:
                return null;
        }
        
        list.Add(go);
        return go;
    }

    GameObject ActivateObject(ref List<GameObject> list, ref GameObject prefab, ref GameObject parent)
    {
        // If there are no objects in the list, create it
        if (list.Count <= 0)
            return CreateObject(ref list, ref prefab, ref parent);

        bool isAllActive = false;
        GameObject go;
        for (int i = 0; i < list.Count; ++i)
        {
            go = list[i];

            // Find an inactive clone to activate
            switch (go.tag)
            {
                case "Enemy":
                case "Food":
                    if (go.activeSelf)
                        isAllActive = true;
                    else
                    {
                        isAllActive = false;
                        go.SetActive(true);
                    }
                    break;

                case "Body":
                    if (go.GetComponent<SpriteRenderer>().enabled)
                        isAllActive = true;
                    else
                    {
                        isAllActive = false;
                        go.GetComponent<CircleCollider2D>().enabled = true;
                        go.GetComponent<Rigidbody2D>().WakeUp();
                        go.GetComponent<SpriteRenderer>().enabled = true;
                    }
                    break;
            }

            if (!isAllActive)
                return go;
        }

        // If the list is full, create a new object
        return CreateObject(ref list, ref prefab, ref parent);
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
        return ActivateObject(ref foodList, ref foodPrefab, ref foodContainer);
    }

    public GameObject GetBody()
    {
        return ActivateObject(ref bodyList, ref bodyPrefab, ref bodyContainer);
    }

    public GameObject GetEnemy(Vector3 position)
    {
        GameObject obj = ActivateObject(ref enemyList, ref enemyPrefab, ref enemyContainer);
        obj.transform.position = position;
        return obj;
    }
    #endregion
}
