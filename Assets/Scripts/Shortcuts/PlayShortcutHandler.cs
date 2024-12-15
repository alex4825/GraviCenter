using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayShortcutHandler : MonoBehaviour
{
    [SerializeField] GameObject littleGC;
    [SerializeField] GameObject middleGC;
    [SerializeField] GameObject bigGC;

    public static GameObject SelectedGC { get; set; }

    private void OnEnable()
    {
        GraviCenter.OnPlacedGC += ClearSelectedGC;
    }

    private void OnDisable()
    {
        GraviCenter.OnPlacedGC -= ClearSelectedGC;
    }

    private void Update()
    {
        HandleObjectSelection(KeyCode.Alpha1, KeyCode.Keypad1, littleGC);
        HandleObjectSelection(KeyCode.Alpha2, KeyCode.Keypad2, middleGC);
        HandleObjectSelection(KeyCode.Alpha3, KeyCode.Keypad3, bigGC);

        // ----- GC deleting ----- //
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (Input.GetKeyDown(KeyCode.Z)) //Alt + Z => destroy last GC
            {
                List<GameObject> GCs = GameManager.Instance.CurrentLevel.GCs;
                if (GCs.Count > 0)
                {
                    Destroyer.DeleteObject(GCs[GCs.Count - 1]);
                    return;
                }
            }
            else if (Input.GetMouseButtonDown(0)) //Alt + left mouse click => destroy GC
            {
                GameObject GC = RaycastTracker.GetPointerObject("GC");

                if (GC != null)
                {
                    Destroyer.DeleteObject(GC);
                    return;
                }
            }
        }

    }

    private void HandleObjectSelection(KeyCode alphaKey, KeyCode keypadKey, GameObject gcPrefab)
    {
        if (Input.GetKeyDown(alphaKey) || Input.GetKeyDown(keypadKey))
        {
            Level currentLevel = GameManager.Instance.CurrentLevel;
            if (SelectedGC)
            {
                bool isCancel = SelectedGC.GetComponent<GraviCenter>().EnergyCost == gcPrefab.GetComponent<GraviCenter>().EnergyCost;
                Destroyer.DeleteObject(SelectedGC);
                if (isCancel)
                    return;
            }
            if (currentLevel.EnergyAmount >= gcPrefab.GetComponent<GraviCenter>().EnergyCost)
            {
                SelectedGC = Instantiate(gcPrefab, Input.mousePosition, Quaternion.identity, currentLevel.transform);
            }
        }
    }

    private void ClearSelectedGC(Transform transformGC)
    {
        SelectedGC = null;
    }
}
