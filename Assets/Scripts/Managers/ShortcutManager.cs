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
    }

}
