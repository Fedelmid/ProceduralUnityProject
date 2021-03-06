﻿using UnityEngine;

public class MouseLock : MonoBehaviour
{
    void Update()
    {
        // Unlock cursor from window
        if (Input.GetKeyDown(KeyCode.C))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void mouseLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
