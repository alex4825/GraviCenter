using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] float duration = 50f;
    [SerializeField] GameObject gradientTop;
    [SerializeField] GameObject gradientBottom;

    private GameObject secondImageObject;

    private Tween moveUpFirst;
    private Tween moveUpSecond;

    private bool isMoving = true;
    private Vector2 lastSize;
    private bool isSizeInitialized = false;

    private void Start()
    {
        lastSize = ((RectTransform)transform).rect.size;
        isSizeInitialized = true;
        StartMovement();
    }
    private void OnEnable()
    {
        if (isSizeInitialized)
        {
            ResumeMovement();
        }
    }

    private void OnDisable()
    {
        PauseMovement();
    }

    private void OnRectTransformDimensionsChange()
    {
        if (!isSizeInitialized)
            return;

        Vector2 currentSize = ((RectTransform)transform).rect.size;
        if (currentSize == lastSize)
        {
            return;
        }
        lastSize = currentSize;

        moveUpFirst.Kill();
        moveUpSecond.Kill();

        transform.position = new Vector2(Screen.width / 2, Screen.height / 2);

        secondImageObject.transform.position = new Vector2(Screen.width / 2, -Screen.height / 2);

        moveUpFirst = transform.DOMoveY(Screen.height * 1.5f, duration, true).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        moveUpSecond = secondImageObject.transform.DOMoveY(Screen.height / 2, duration, true).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        ResizeGradients();
    }

    private void StartMovement()
    {
        secondImageObject = Instantiate(gameObject, transform.parent);
        Destroy(secondImageObject.GetComponent<MoveBackground>());
        secondImageObject.transform.position = new Vector2(Screen.width / 2, -Screen.height / 2);
        secondImageObject.transform.SetSiblingIndex(0);

        moveUpFirst = transform.DOMoveY(Screen.height * 1.5f, duration, true).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
        moveUpSecond = secondImageObject.transform.DOMoveY(Screen.height / 2, duration, true).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);

        ResizeGradients();
    }

    private void PauseMovement()
    {
        moveUpFirst.Pause();
        moveUpSecond.Pause();
        isMoving = false;
    }
    private void ResumeMovement()
    {
        if (!isMoving)
        {
            moveUpFirst.Play();
            moveUpSecond.Play();
            isMoving = true;
        }
    }

    private void ResizeGradients()
    {
        RectTransform rectTop = gradientTop.GetComponent<RectTransform>();
        RectTransform rectBottom = gradientBottom.GetComponent<RectTransform>();

        rectTop.sizeDelta = new Vector2(rectTop.sizeDelta.x, Screen.height * 0.3f);
        rectBottom.sizeDelta = new Vector2(rectBottom.sizeDelta.x, Screen.height * 0.3f);
    }
}
