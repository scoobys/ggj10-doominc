using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    public InputField _input;
    public Game _global;


    private void Awake()
    {
        _input = GameObject.FindGameObjectWithTag("Input").GetComponentInChildren<InputField>();
        _global = GameObject.FindGameObjectWithTag("Game").GetComponentInChildren<Game>();

    }

    public void LoadByIndex(int sceneIndex)
    {
        _global.villageName = _input.text;
        Debug.Log(_global.villageName);
        SceneManager.LoadScene(sceneIndex);
    }
}

