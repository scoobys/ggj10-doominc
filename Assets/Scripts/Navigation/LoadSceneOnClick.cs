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
        try
        {
            _input = GameObject.FindGameObjectWithTag("Input").GetComponentInChildren<InputField>();
            _global = GameObject.FindGameObjectWithTag("Game").GetComponentInChildren<Game>();
        }
        catch (Exception) { }

    }

    public void LoadByIndex(int sceneIndex)
    {
        if (_global != null && _input != null)
        {
            _global.villageName = _input.text;
            _global.clickHouseEnabled = true;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}

