using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Spark : MonoBehaviour
{
    [SerializeField] int value = 100;
    public int Value { get { return value; } }

    private void Start()
    {
        Animator.ScaleAppear(transform);
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(transform.position));
    }
}
