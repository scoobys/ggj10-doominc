using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyController : MonoBehaviour {

    Animator thisAnimator;
    SpriteRenderer sky;
    float targetGBValue;
    float currentGBValue;
    float colorChangeStepSize;

    void Awake () {
        thisAnimator = GetComponent<Animator>();
        sky = transform.Find("SkyCircle").GetComponent<SpriteRenderer>();
        Debug.Log("Found sky " + (sky != null));
    }

    void Start()
    {
        targetGBValue = 1.0f;
        currentGBValue = 1.0f;
        colorChangeStepSize = 1.0f;
    }

    void Update()
    {
        float change = 0.1f * Time.deltaTime;
        if (currentGBValue > targetGBValue)
        {
            currentGBValue -= change;
            if (currentGBValue < targetGBValue)
            {
                currentGBValue = targetGBValue;
            }
            // targetGBValue -= 0.01f * Time.deltaTime;
        }
        else if (currentGBValue < targetGBValue)
        {
            currentGBValue += change;
            if (currentGBValue > targetGBValue)
            {
                currentGBValue = targetGBValue;
            }
        }
        sky.color = new Color(1.0f, currentGBValue, currentGBValue, 1.0f);
    }

    public void NextDay(float relativeDoom) {
        thisAnimator.SetTrigger("NextDay");
        float gb = 1.0f - relativeDoom;
        if (relativeDoom > 0.5)
        {
            // sky.color = new Color(1.0f, gb, gb, 1.0f);
            targetGBValue = gb;
        }
        else
        {
            targetGBValue = 1.0f;
        }
    }
}
