using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Destroyer
{
    private static float speedDepth = 0.2f;
    public static void DeleteGC(GameObject objGC)
    {
        if (objGC == null)
            return;

        GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(objGC.transform.position));
        GameManager.Instance.CurrentLevel.GCs.Remove(objGC);

        if (objGC.GetComponent<GraviCenter>().IsAttracts)
        {
            objGC.transform.DOScale(Vector3.zero, speedDepth)
                .OnComplete(() => { UnityEngine.Object.Destroy(objGC); });
        }
        else
        {
            MaterialChanger.SetTransparency(objGC, 0, speedDepth);
            objGC.transform.DOScale(objGC.transform.localScale * 4, speedDepth).SetEase(Ease.InCubic)
                .OnComplete(() => { UnityEngine.Object.Destroy(objGC); });
        }
    }
    public static void DeleteEnergy(Transform energyTransform)
    {
        energyTransform.DOScale(Vector3.zero, speedDepth)
                .OnComplete(() => { UnityEngine.Object.Destroy(energyTransform.gameObject); });
    }
}
