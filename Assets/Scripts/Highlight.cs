using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlight : MonoBehaviour
{
    private bool isActive = false;
    private Outline outline;
    
    void Start()
    {
        outline = GetComponent<Outline>();
        if(outline == null)
        {
            gameObject.AddComponent<Outline>();
            outline.OutlineColor = new Color(195, 126, 0);
            outline.OutlineMode = Outline.Mode.OutlineAll;
            outline.OutlineWidth = 7f;
        }

    }

    public void EnableOutline()
    {
        outline.UpdateMaterialProperties();
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        if(!isActive)
        {
            outline.enabled = false;
        }
    }

    public void SetIsActive(bool flag)
    {
        isActive = flag;
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
