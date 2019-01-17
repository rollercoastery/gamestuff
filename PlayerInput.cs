using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Manages all player gameplay controls.
*/
public class PlayerInput : MonoBehaviour {
    
    public GameObject sliderBuildMode;
    Slider slider;
    Animator anim;

    void Awake()
    {
        slider = sliderBuildMode.GetComponent<Slider>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        BuildMode();
        if (GameplayData.gd.isBuildMode)
            anim.speed = 0.4f;
        else
            anim.speed = 0.8f;
    }

    float juiceDelayTime;   // Delay time before juice starts regenerating
    void BuildMode()
    {
        // Slow motion when pressing spacebar, this goes into building mode
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameplayData.gd.isBuildMode = !GameplayData.gd.isBuildMode;
        }

        if (slider.value <= 0f)
        {
            GameplayData.gd.isBuildMode = false;
        }

        if (GameplayData.gd.isBuildMode)
        {
            GameplayData.gd.gameSpeed = 0.2f;
            juiceDelayTime = 0f;
            slider.value -= Time.deltaTime * 0.2f;
        }
        else
        {
            GameplayData.gd.gameSpeed = 1f;

            if (slider.value < 1f && juiceDelayTime < 3f)
            {
                HelperFunctions.hf.Timer(ref juiceDelayTime, 3f);
            }
            else
            {
                slider.value += Time.deltaTime * 0.2f;
            }
        }
    }

    #region MOVEMENT
    void FixedUpdate()
    {
        Rotation();
        Move();

        if (GameplayData.gd.foodCollected >= 1)
            Expand();

        /*if (Input.GetKeyUp(KeyCode.N))
        {
            ObjectManager.om.RemoveObject(ObjectManager.om.bodyList[GameplayData.gd.bodyCount-1].gameObject);
        }*/
    }

    public GameObject cursor;
    float angle;
    void Rotation()
    {
        // Get angle between player and cursor positions
        Vector3 vectorToTarget = cursor.transform.position - transform.position;
        angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;

        // Smooth transition of rotation to cursor
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, GameplayData.gd.dTime * GameplayData.gd.turnSpeed);
    }

    void Move()
    {
        transform.Translate(Vector3.up * GameplayData.gd.moveSpeed * GameplayData.gd.dTime);
    }
    #endregion

    void Expand()
    {
        if (Input.GetKeyUp(KeyCode.B))
        {
            GameplayData.gd.foodCollected -= 1;
            GameplayData.gd.bodyCount += 1;
            ObjectManager.om.GetBody();
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 25), "Body: " + GameplayData.gd.bodyCount);
        GUI.Label(new Rect(10, 35, 200, 25), "Food: " + GameplayData.gd.foodCollected);
        GUI.Label(new Rect(10, 60, 200, 25), "HP: " + GameplayData.gd.health);
        //GUI.Label(new Rect(10, 35, 200, 25), angle.ToString());
    }
}
