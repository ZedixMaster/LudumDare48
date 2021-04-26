using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HyperspeedMessage : MonoBehaviour
{
    public float timeToFade;
    private float currentFadeTime;

    private TextMeshProUGUI background;

    void Awake()
    {
        background = GetComponent<TextMeshProUGUI>();
        background.color = new Color(1, 1, 1, 0);
        currentFadeTime = timeToFade;
    }

    void Update()
    {
        if (currentFadeTime > 0)
        {
            background.color = new Color(1, 1, 1, (currentFadeTime / timeToFade));
            currentFadeTime -= Time.deltaTime;
        }
    }
}
