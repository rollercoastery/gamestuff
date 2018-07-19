using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Common functions that reduces repeat logic
*/
public class HelperFunctions : MonoBehaviour {

    public static HelperFunctions hf;

    void Awake()
    {
        if (hf == null)
        {
            DontDestroyOnLoad(gameObject);
            hf = this;
        }
        else if (hf != this)
        {
            Destroy(gameObject);
        }
        //MakeSingleton(hf, gameObject);
    }

    // Common count up timer
    public void Timer(ref float current, float end)
    {
        if (current < end)
            current += GameplayData.gd.dTime;
        else
            current = 0f;
    }
    // Common count down timer
    public void TimerDown(ref float current, float init)
    {
        if (current > 0f)
            current -= GameplayData.gd.dTime;
        else
            current = init;
    }

    // Common scaling animation
    public void Scaler(ref Vector3 start, ref Vector3 end)
    {

    }

    // Creates a singleton, not working..
    public void MakeSingleton(System.Type type, GameObject obj)
    {
        if (type == null)
        {
            DontDestroyOnLoad(obj);
            type = obj.GetType();
        }
        else if (type != obj.GetType())
        {
            Destroy(obj);
        }
    }
}
