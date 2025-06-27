using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static void ChangeTimeScale(float scale)
    {
        Time.timeScale = scale; 
        Debug.Log("New time scale - " +  scale);
    }

    public static float GetTimeScale()
    {
        return Time.timeScale; 
    }
}
