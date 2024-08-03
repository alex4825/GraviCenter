using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorGC : MonoBehaviour
{    
    private Image outlineImg;
    [SerializeField] GameObject graviCenter;
    [SerializeField] Color normalColor = Color.black;
    [SerializeField] Color hoverColor = Color.white;

    private void Start()
    {
        outlineImg = GetComponent<Image>();
        outlineImg.color = normalColor;
    }

    public void OnMouseEnter()
    {
        outlineImg.color = hoverColor;
        outlineImg.transform.localScale *= 1.05f;
    }

    public void OnMouseExit()
    {
        outlineImg.color = normalColor;
        outlineImg.transform.localScale /= 1.05f;
    }
}
