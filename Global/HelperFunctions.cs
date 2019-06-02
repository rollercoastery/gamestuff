using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Common functions that reduces repeat logic.
    Needs to be Scriptable Object!
*/
public class HelperFunctions : MonoBehaviour {

    public GameplayData gd;
    
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

    // Count up timer. Returns true once timer has completed, else returns false
    public bool Timer(ref float current, float end)
    {
        if (current < end)
        {
            current += Time.deltaTime * gd.gameSpeed;
            return false;
        }
        else
        {
            current = 0f;
            return true;
        }
    }
    // Common count down timer
    public void TimerDown(ref float current, float init)
    {
        if (current > 0f)
            current -= Time.deltaTime * gd.gameSpeed;
        else
            current = init;
    }

    // Warp objects to the other side of the playable field
    public void WarpObject(ref Vector3 currentPosition, float screenWidth, float screenHeight)
    {
        Vector3 newPosition = currentPosition;
        if (currentPosition.x >= screenWidth || currentPosition.x <= -screenWidth)
        {
            newPosition.x *= -1f;
        }
        if (currentPosition.y >= screenHeight || currentPosition.y <= -screenHeight)
        {
            newPosition.y *= -1f;
        }
        currentPosition = newPosition;
    }

    // Common scaling animation
    public Vector3 Scaler(Vector3 start, Vector3 end, float time)
    {
        return Vector3.Lerp(start, end, Time.deltaTime * gd.gameSpeed * time);
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
