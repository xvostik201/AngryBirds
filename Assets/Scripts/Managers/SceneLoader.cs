using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(int sceneToLoad = 0, bool loadCurrentScene = false, bool loadNextScene = false)
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int targetIndex;

        if (loadCurrentScene)
        {
            targetIndex = currentIndex;
        }
        else if (loadNextScene)
        {
            targetIndex = currentIndex + 1;
        }
        else
        {
            targetIndex = sceneToLoad >= 0
                ? sceneToLoad
                : currentIndex;
        }

        if (targetIndex < 0 || targetIndex >= sceneCount)
        {
            targetIndex = 1;
        }

        TimeManager.ChangeTimeScale(1f);

        SceneManager.LoadScene(targetIndex);
    }
}
