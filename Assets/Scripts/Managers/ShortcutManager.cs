using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    [SerializeField] GameObject littleGC;
    [SerializeField] GameObject middleGC;
    [SerializeField] GameObject bigGC;

    private GameObject selectedGC = null;

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

        #region GC deleting
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
        {
            List<Transform> graviCenters = GameManager.Instance.CurrentLevel.GCs;
            int count = graviCenters.Count;

            if (count > 0)
            {
                graviCenters[count - 1].GetComponent<GraviCenter>().DeleteGC();
            }
        }
        #endregion
    }

    private void HandleObjectSelection(KeyCode alphaKey, KeyCode keypadKey, GameObject gcPrefab)
    {
        if (Input.GetKeyDown(alphaKey) || Input.GetKeyDown(keypadKey))
        {
            if (selectedGC)
            {
                Destroy(selectedGC);
            }
            selectedGC = Instantiate(gcPrefab, Input.mousePosition, gcPrefab.transform.rotation);
        }
    }

    private void ClearSelectedGC()
    {
        selectedGC = null;
    }
}
