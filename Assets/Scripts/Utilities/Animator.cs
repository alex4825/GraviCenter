using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour
{
    [SerializeField] Vector3Int anglePerSecond; 
    [SerializeField] float animationDuration = 3f;
    [SerializeField] float moveDistance = 0.1f;
    [SerializeField] float speedAppear = 0.4f;

    private void OnEnable()
    {
        BallController.OnBallCreated += ScaleAppear;
        BallController.OnBallDead += ScaleDisappear;

        GraviCenter.OnTakedGC += ScaleAppear;
        GraviCenter.OnTakedGC += RotateRings;
        GraviCenter.OnPlacedGC += MoveUpDown;

        Energy.OnEnergyCreated += ScaleAppear;
        Energy.OnEnergyCreated += MoveUpDown;
        Energy.OnEnergyCreated += Rotate;
    }

    private void OnDisable()
    {
        BallController.OnBallCreated -= ScaleAppear;
        BallController.OnBallDead -= ScaleDisappear;

        GraviCenter.OnTakedGC -= ScaleAppear;
        GraviCenter.OnTakedGC -= RotateRings;
        GraviCenter.OnPlacedGC -= MoveUpDown;

        Energy.OnEnergyCreated -= ScaleAppear;
        Energy.OnEnergyCreated -= MoveUpDown;
        Energy.OnEnergyCreated -= Rotate;
    }
    private void MoveUpDown(Transform objTransform)
    {
        objTransform.DOMoveY(objTransform.position.y + moveDistance, animationDuration)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    private void Rotate(Transform objTransform)
    {
        objTransform.DORotate(new Vector3(0, 180, 0), animationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
    }

    private void RotateRings(Transform transformGC)
    {
        List<Transform> ringTransforms = Searcher.FindChildsWithTag(transformGC, "RingGC");

        for (int i = 0; i < ringTransforms.Count; i++)
        {
            ringTransforms[i].DORotate(anglePerSecond / (i + 1), 1f, RotateMode.FastBeyond360)
              .SetLoops(-1, LoopType.Incremental)
              .SetEase(Ease.Linear);
        }
    }

    private void ScaleAppear(Transform objTransform)
    {
        objTransform.localScale = Vector3.zero;
        objTransform.DOScale(Vector3.one, speedAppear).SetEase(Ease.Linear);
    }

    private void ScaleDisappear(Transform objTransform)
    {
        objTransform.DOScale(Vector3.zero, speedAppear).SetEase(Ease.Linear);
    }
}
