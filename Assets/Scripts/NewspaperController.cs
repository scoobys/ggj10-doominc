using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewspaperController : MonoBehaviour {

    Animator thisAnimator;
    TextMeshProUGUI nameText;
    TextMeshProUGUI headlineText;

    void Awake () {
        thisAnimator = GetComponent<Animator>();
        Component[] components = GetComponentsInChildren(typeof(TextMeshProUGUI));
        foreach (Component c in components) {
            string name = c.gameObject.name;
            if (name == "Name") {
                nameText = (TextMeshProUGUI) c;
            } else if (name == "Header") {
                headlineText = (TextMeshProUGUI) c;
            }
        }
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    public void Open(string headline) {
        headlineText.text = headline;
        thisAnimator.SetTrigger("Open");
    }

    public void Close() {
        thisAnimator.SetTrigger("Close");
    }
}
