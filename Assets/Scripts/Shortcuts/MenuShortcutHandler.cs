using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShortcutHandler : MonoBehaviour
{
    public delegate void MenuEnableAction();
    public static event MenuEnableAction OnMenuEnable;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnMenuEnable?.Invoke();
        }
    }
}
