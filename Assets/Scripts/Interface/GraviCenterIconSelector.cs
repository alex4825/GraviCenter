using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraviCenterIconSelector : MonoBehaviour
{
    [SerializeField] GameObject gcObject;
    [SerializeField] Color normalColor = Color.black;
    [SerializeField] Color hoverColor = Color.white;

    private Image outlineImg;

    private void Start()
    {
        outlineImg = GetComponent<Image>();
        outlineImg.color = normalColor;
    }

    public void OnMouseDown()
    {
        Level currentLevel = GameManager.Instance.CurrentLevel;
        if (currentLevel.EnergyAmount >= gcObject.GetComponent<GraviCenter>().EnergyCost)
        {
            GameObject GC = Instantiate(gcObject, Input.mousePosition, gcObject.transform.rotation, currentLevel.transform);
            PlayShortcutHandler.SelectedGC = GC;
        }
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
