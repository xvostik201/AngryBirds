using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(int sceneToLoad)
    {
        if(sceneToLoad == -1)
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
            SceneManager.LoadScene(sceneToLoad);
    }
}
