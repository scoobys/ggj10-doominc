using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioTowerController : MonoBehaviour {

    private Animator thisAnimator;
    private TextMeshPro towerText;
    private bool isTransmissionInProgress = false;
    private float nextTransmissionCloseTime = float.PositiveInfinity;

    void Awake () {
        Component[] components = GetComponentsInChildren(typeof(TextMeshPro));
        foreach (Component c in components) {
            string name = c.gameObject.name;
            if (name == "Text") {
                towerText = (TextMeshPro) c;
                break;
            }
        }

        thisAnimator = GetComponent<Animator>();
    }

    void Update () {
        float t = Time.time;
        if (nextTransmissionCloseTime < t) {
            TransmissionClose();
            nextTransmissionCloseTime = float.PositiveInfinity;
        }
    }

    public void TransmissionOpen(string text) {
        if (isTransmissionInProgress) {
            return;
        }
        isTransmissionInProgress = true;
        towerText.text = text;
        nextTransmissionCloseTime = Time.time + Game.instance.transmissionOpenDuration;
        thisAnimator.SetTrigger("Open");
    }

    public void TransmissionClose() {
        if (!isTransmissionInProgress) {
            return;
        }
        thisAnimator.SetTrigger("Close");
        isTransmissionInProgress = false;
    }
}
