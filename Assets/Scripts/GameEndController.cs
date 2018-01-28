using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndController : MonoBehaviour {

    private GameObject youWin;
    private GameObject youLose;
    private GameObject menuButton;

    void Awake () {
        youWin = GameObject.FindGameObjectWithTag("YouWin");
        youLose = GameObject.FindGameObjectWithTag("YouLose");
        {
            Component[] components = GetComponentsInChildren(typeof(Button));
            foreach (Component c in components) {
                string name = c.gameObject.name;
                if (name == "MainMenuButton") {
                    menuButton = c.gameObject;
                }
            }
        }

        SetButtonsActive(false);
        youWin.SetActive(false);
        youLose.SetActive(false);
    }

    void SetButtonsActive(bool active) {
        menuButton.SetActive(active);
    }

    public void Win() {
        SetButtonsActive(true);
        youWin.SetActive(true);
    }

    public void Lose() {
        SetButtonsActive(true);
        youLose.SetActive(true);
    }
}
