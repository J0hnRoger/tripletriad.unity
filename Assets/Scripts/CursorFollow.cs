using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
    }
}
