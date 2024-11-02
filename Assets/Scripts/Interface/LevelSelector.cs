using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] int part = 1;
    [SerializeField] int number;
 
    private Color normalColor;
    private Color hoverColor;

    public delegate void LevelSelectedAction(int partNumber, int levelNumber);
    public static event LevelSelectedAction OnLevelSelected;

    private void Start()
    {
        normalColor = GetComponent<Image>().color;
        hoverColor = normalColor * 0.9f;

        TextMeshProUGUI textTMP = GetComponentInChildren<TextMeshProUGUI>();
        textTMP.text = number.ToString();
    }
    public void OnMouseDown()
    {
        OnLevelSelected?.Invoke(part, number);
    }

    public void OnMouseEnter()
    {
        GetComponent<Image>().color = hoverColor;        
        GetComponent<Image>().transform.DOScale(1.1f, 0.2f);
    }

    public void OnMouseExit()
    {
        GetComponent<Image>().color = normalColor;
        GetComponent<Image>().transform.DOScale(1.0f, 0.2f);
    }
}
