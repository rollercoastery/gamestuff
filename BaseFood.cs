using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Base food type with common logic and behaviors.
*/
public class BaseFood : MonoBehaviour {

    public enum FoodType
    {
        Default,
        Sweet,
        Salty,
        Bitter,
        Sour
    }

    public FoodType foodType;

    //Vector3 defaultScale;
    float currentTimer;
    TextMesh textMesh;

    void OnEnable ()
    {
        // Reset everything
        //defaultScale = transform.localScale = new Vector3(1f, 1f, 1f);
        textMesh = transform.GetChild(0).GetComponent<TextMesh>();
        currentTimer = 0f;
        textMesh.text = "";
    }

    void Update()
    {
        HelperFunctions.hf.Timer(ref currentTimer, 11f);
        textMesh.text = Mathf.FloorToInt(11f - currentTimer).ToString();

        if (currentTimer >= 11f)
        {
            textMesh.text = "";
            ObjectManager.om.RemoveObject(gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameplayData.gd.foodCollected += 1;
            ObjectManager.om.RemoveObject(this.gameObject);
        }
    }
}
