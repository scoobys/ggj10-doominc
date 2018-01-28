using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class RadioTowerController : MonoBehaviour {

    private Animator thisAnimator;
    private TextMeshPro towerText;
    private bool isTransmissionInProgress = false;
    private float nextTransmissionCloseTime = float.PositiveInfinity;
    private float lastTransmissionTime;

    private System.Random rnd;

    private string[] transmissions;

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

        lastTransmissionTime = 0.0f;
        rnd = new System.Random();
        transmissions = (Resources.Load("transmissions") as TextAsset).text.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
    }

    void Update () {
        float t = Time.time;
        if (nextTransmissionCloseTime < t) {
            TransmissionClose();
            nextTransmissionCloseTime = float.PositiveInfinity;
        }
        // else if (t - lastTransmissionTime > rnd.Next(10, 30))
        else if (t - lastTransmissionTime > 3)
        {
            lastTransmissionTime = t;
            string transmission = transmissions[rnd.Next(0, transmissions.Length - 1)];
            // TransmissionOpen(transmission);
            Debug.Log(transmission);
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
