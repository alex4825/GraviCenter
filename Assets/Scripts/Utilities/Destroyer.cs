using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class Destroyer
{

    private static float speedDepth = 0.2f;
    public static void DeleteGC(GraviCenter GC)
    {
        if (GC.gameObject == null)
            return;

        GameManager.Instance.CurrentLevel.Floors.Add(CoordEditor.RoundToHalf(GC.transform.position));
        GameManager.Instance.CurrentLevel.GCs.Remove(GC);

        if (GC.IsAttracts)
        {
            GC.transform.DOScale(Vector3.zero, speedDepth)
                .OnComplete(() => { Object.Destroy(GC.gameObject); });
        }
        else
        {
            MaterialChanger.SetTransparency(GC.gameObject, 0, speedDepth);
            GC.transform.DOScale(GC.transform.localScale * 4, speedDepth).SetEase(Ease.InCubic)
                .OnComplete(() => { Object.Destroy(GC.gameObject); });
        }
    }
}
