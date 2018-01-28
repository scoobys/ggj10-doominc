using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewspaperController : MonoBehaviour {

    private bool isOpen;
    private Animator thisAnimator;
    private TextMeshPro nameText;
    private TextMeshPro headlineText;

    // Debug stuff
    private float nextUpdateTime = float.NegativeInfinity;
    private float updateDelta = 5.0f;

    void Awake () {
        thisAnimator = GetComponent<Animator>();
        Component[] components = GetComponentsInChildren(typeof(TextMeshPro));
        foreach (Component c in components) {
            string name = c.gameObject.name;
            if (name == "Name") {
                nameText = (TextMeshPro) c;
            } else if (name == "Header") {
                headlineText = (TextMeshPro) c;
            }
        }
        Close();
    }

    void Update() {
        float t = Time.time;
        if (nextUpdateTime < t) {
            Open("Headline: " + t);
            nextUpdateTime = t + updateDelta;
        }
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    public void Open(string headline) {
        if (isOpen) {
            return;
        }
        headlineText.text = headline;
        thisAnimator.SetTrigger("Open");
        isOpen = true;
    }

    public void Close() {
        if (!isOpen) {
            return;
        }
        isOpen = false;
        thisAnimator.SetTrigger("Close");
    }
}
