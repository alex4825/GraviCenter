using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorGC : MonoBehaviour
{
    [SerializeField] Vector3Int anglePerSecond;
    [SerializeField] float animationDuration = 3f;
    [SerializeField] float moveDistance = 0.6f;
    private void Start()
    {
        List<Transform> ringTransforms = Searcher.FindChildsWithTag(transform, "RingGC");

        for (int i = 0; i < ringTransforms.Count; i++)
        {
            ringTransforms[i].DORotate(anglePerSecond / (i + 1), 1f, RotateMode.FastBeyond360)
              .SetLoops(-1, LoopType.Incremental)
              .SetEase(Ease.Linear);
        }
    }

    private void OnEnable()
    {
        GraviCenter.OnPlacedGC += MoveUpDown;
    }

    private void OnDisable()
    {
        GraviCenter.OnPlacedGC -= MoveUpDown;
    }
    private void MoveUpDown()
    {
        transform.DOMoveY(transform.position.y + moveDistance, animationDuration)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
