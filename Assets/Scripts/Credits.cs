using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public float timeToFade;
    private float currentFadeTime;

    private Image background;

    void Awake()
    {
        background = GetComponent<Image>();
        background.color = new Color(0, 0, 0, 0);
        currentFadeTime = 0;
    }
    
    void Update()
    {
        if (currentFadeTime < timeToFade)
        {
            background.color = new Color(0, 0, 0, (currentFadeTime / timeToFade));
            currentFadeTime += Time.deltaTime;
        }
    }
}
