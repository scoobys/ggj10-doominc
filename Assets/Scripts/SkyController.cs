using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

    Animator thisAnimator;

    void Awake () {
        thisAnimator = GetComponent<Animator>();
    }

    public void NextDay() {
        thisAnimator.SetTrigger("NextDay");
    }
}
