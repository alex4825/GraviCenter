using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
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
            if (Input.GetKeyDown(KeyCode.Z))
            {
                List<GameObject> graviCenters = GameManager.Instance.CurrentLevel.GCs;
                int count = graviCenters.Count;

                if (count > 0)
                {
                    Destroyer.DeleteGC(graviCenters[count - 1]);
                }
            }
            else if (Input.GetMouseButtonDown(0)) //Alt + left mouse click => destroy GC
            {
                GameObject GC = RaycastTracker.GetPointerObject("GC");

                if (GC != null)
                {
                    Destroyer.DeleteGC(GC);
                    return;
                }
            }
        }

    }

    private void HandleObjectSelection(KeyCode alphaKey, KeyCode keypadKey, GameObject gcPrefab)
    {
        if (Input.GetKeyDown(alphaKey) || Input.GetKeyDown(keypadKey))
        {
            if (SelectedGC)
            {
                Destroyer.DeleteGC(SelectedGC);
            }
            if (GameManager.Instance.CurrentLevel.EnergyAmount >= gcPrefab.GetComponent<GraviCenter>().EnergyCost)
            {
                SelectedGC = Instantiate(gcPrefab, Input.mousePosition, gcPrefab.transform.rotation);
            }
        }
    }

    private void ClearSelectedGC(Transform transformGC)
    {
        SelectedGC = null;
    }
}
