using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewspaperController : MonoBehaviour {

    private bool isOpen;
    private Animator thisAnimator;
    private TextMeshPro nameText;
    private TextMeshPro headlineText;


    private bool isDelayedOpenInProgress = false;
    private float nextDelayedOpenTime = float.PositiveInfinity;
    private string delayedOpenHeadline = "";

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

    // Debug stuff
    void Update() {
        float t = Time.time;
        if (isDelayedOpenInProgress) {
            if (nextDelayedOpenTime < t) {
                Open(delayedOpenHeadline);
                isDelayedOpenInProgress = false;
                nextDelayedOpenTime = float.PositiveInfinity;
            }
        }
    }

    public void OpenDelayed(float offset, string headline) {
        if (isDelayedOpenInProgress) {
            return;
        }

        delayedOpenHeadline = headline;
        nextDelayedOpenTime = Time.time + offset;
        isDelayedOpenInProgress = true;
    }

    public void Open(string headline) {
        if (isOpen) {
            return;
        }
        Debug.Assert(Game.instance != null);
        nameText.text = Game.instance.villageName + "ville Times";
        headlineText.text = headline.ToUpper();
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
