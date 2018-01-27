using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {

    private float targetDegrees;

    public float defaultValue;
    public float minValue;
    public float maxValue;
    public float offsetDegrees;
    public float minDegrees;
    public float maxDegrees;
    public float moveSpeed;

    // Debug stuff
    //private float updateDelta = 3.0f;
    //private float nextUpdate = float.NegativeInfinity;

    void Awake () {
        SetValue(defaultValue);
    }

    void Update () {
        // Debug stuff
        //float t = Time.time;

        //if (nextUpdate < t) {
        //    SetValue(Random.Range(minValue, maxValue));
        //    nextUpdate = t + updateDelta;
        //}

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetDegrees);
        gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, targetRotation, moveSpeed * Time.deltaTime);
    }

    public void SetValue(float val) {
        float clampedVal = Mathf.Clamp(val, minValue, maxValue);
        float valueFraction = (clampedVal - minValue) / (maxValue - minValue);
        targetDegrees = -(offsetDegrees + minDegrees + (maxDegrees - minDegrees) * valueFraction);
    }
}
