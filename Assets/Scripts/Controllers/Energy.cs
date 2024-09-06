using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Energy : MonoBehaviour
{
    [SerializeField] int energyValue = 100;

    [SerializeField] float animationDuration = 3f;
    [SerializeField] float moveDistance = 0.1f;
    public int EnergyValue { get { return energyValue; } set { energyValue = value; } }

    private void Start()
    {
        transform.DORotate(new Vector3(0, 180, 0), animationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);

        transform.DOMoveY(transform.position.y + moveDistance, animationDuration)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
