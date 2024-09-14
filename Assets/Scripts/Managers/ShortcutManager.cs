using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    [SerializeField] GameObject littleGC;
    [SerializeField] GameObject middleGC;
    [SerializeField] GameObject bigGC;

    private void Update()
    {
        #region GC creating

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(littleGC, Input.mousePosition, littleGC.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instantiate(middleGC, Input.mousePosition, middleGC.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            Instantiate(bigGC, Input.mousePosition, bigGC.transform.rotation);
        }

        #endregion

        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.Z))
        {
            List<Transform> graviCenters = GameManager.CurrentLevel.GCs;
            int count = graviCenters.Count;

            if (count > 0)
            {
                graviCenters[count - 1].GetComponent<GraviCenter>().DeleteGC();
            }
        }
    }
}
