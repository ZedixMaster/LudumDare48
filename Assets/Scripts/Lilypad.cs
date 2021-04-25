using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    private Outline outline;
    public GameObject inventoryItem;

    void Awake()
    {
        outline = GetComponent<Outline>();
        inventoryItem.GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
