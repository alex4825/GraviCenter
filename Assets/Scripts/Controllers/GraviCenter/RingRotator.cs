using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotator : MonoBehaviour
{
    [SerializeField] Vector3Int anglePerSecond;
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
}
