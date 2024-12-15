using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Animator
{
    private static Vector3Int anglePerSecond = new Vector3Int(60, 180, 120);
    private static float animationDuration = 3f;
    private static float moveDistance = 0.1f;
    private static float speedAppear = 0.4f;

    /*private void OnEnable()
    {
        BallController.OnBallCreated += ScaleAppear;
        BallController.OnBallFell += ScaleDisappear;
        Level.OnBallMovedToStart += ScaleAppear;

        GraviCenter.OnTakedGC += ScaleAppear;
        GraviCenter.OnTakedGC += RotateRings;
        GraviCenter.OnPlacedGC += MoveUpDown;

        Spark.OnSparkCreated += ScaleAppear;
        Spark.OnSparkCreated += MoveUpDown;
        Spark.OnSparkCreated += Rotate;
    }

    private void OnDisable()
    {
        BallController.OnBallCreated -= ScaleAppear;
        BallController.OnBallFell -= ScaleDisappear;
        Level.OnBallMovedToStart -= ScaleAppear;

        GraviCenter.OnTakedGC -= ScaleAppear;
        GraviCenter.OnTakedGC -= RotateRings;
        GraviCenter.OnPlacedGC -= MoveUpDown;

        Spark.OnSparkCreated -= ScaleAppear;
        Spark.OnSparkCreated -= MoveUpDown;
        Spark.OnSparkCreated -= Rotate;
    }*/
    public static void MoveUpDown(Transform objTransform)
    {
        objTransform.DOMoveY(objTransform.position.y + moveDistance, animationDuration)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    public static void Rotate(Transform objTransform)
    {
        objTransform.DORotate(new Vector3(0, 180, 0), animationDuration, RotateMode.FastBeyond360)
                .SetLoops(-1, LoopType.Incremental)
                .SetEase(Ease.Linear);
    }

    public static void RotateRings(Transform transformGC)
    {
        List<Transform> ringTransforms = Searcher.FindChildsWithTag(transformGC, "RingGC");

        for (int i = 0; i < ringTransforms.Count; i++)
        {
            ringTransforms[i].DORotate(anglePerSecond / (i + 1), 1f, RotateMode.FastBeyond360)
              .SetLoops(-1, LoopType.Incremental)
              .SetEase(Ease.Linear);
        }
    }

    public static void ScaleAppear(Transform objTransform)
    {
        objTransform.localScale = Vector3.zero;
        objTransform.DOScale(Vector3.one, speedAppear).SetEase(Ease.Linear);
    }

    public static void ScaleDisappear(Transform objTransform)
    {
        objTransform.DOScale(Vector3.zero, speedAppear).SetEase(Ease.Linear);
    }
}
