using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Destroyer
{
    private static float speedDepth = 0.2f;
    /*public static void DeleteGC(GameObject objGC)
    {
        if (objGC == null)
        {
            Debug.LogError("Can't delete GC. GameObject " + objGC.name + " doesn't exists or hasn't been initialized");
            return;
        }
        Level level = GameManager.Instance.CurrentLevel;
        level.Floors.Add(CoordEditor.RoundToHalf(objGC.transform.position));
        //level.GCs.Remove(objGC);

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
    }*/
    public static void DeleteObject(GameObject obj)
    {
        if (obj == null)
        {
            Debug.LogError("Can't delete GC. GameObject " + obj.name + " doesn't exists or hasn't been initialized");
            return;
        }
        obj.transform.DOScale(Vector3.zero, speedDepth)
                .OnComplete(() => { UnityEngine.Object.Destroy(obj); });
    }

    public static void DeleteObjectList(List <GameObject> objList)
    {
        for (int i = 0; i< objList.Count; i++)
        {
            DeleteObject(objList[i]);
        }
    }
}
