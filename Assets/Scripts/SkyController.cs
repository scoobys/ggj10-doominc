using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

    Animator thisAnimator;
    SpriteRenderer sky;
    // float targetGBValue;
    // float currentGBValue;
    // float colorChangeStepSize;

    void Awake () {
        thisAnimator = GetComponent<Animator>();
        sky = transform.Find("SkyCircle").GetComponent<SpriteRenderer>();
        Debug.Log("Found sky " + (sky != null));
    }

    void Start()
    {
        // targetGBValue = 1.0f;
        // currentGBValue = 1.0f;
        // colorChangeStepSize = 1.0f;
    }

    void Update()
    {
        // if (currentGBValue > targetGBValue)
        // {

        //     targetGBValue -= 0.01f * Time.deltaTime;
        // }
    }

    public void NextDay(float relativeDoom) {
        thisAnimator.SetTrigger("NextDay");
        float gb = 1.0f - relativeDoom;
        if (relativeDoom > 0.5)
        {
            sky.color = new Color(1.0f, gb, gb, 1.0f);
        }
        // else
        // {
        //     targetGBValue = 1.0f;
        // }
    }
}
