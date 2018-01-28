using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

    public static void LoadByIndex(int sceneIndex)
    {
        Debug.Log("LoadScene " + sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
