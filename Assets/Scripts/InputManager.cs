using System;
using System.Collections;
using TripleTriad.Battle;
using TripleTriad.Engine;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager> 
{
    public Action<Vector2> OnStartTouch;
    public Action<Vector2> OnEndTouch;
    public Action<Vector2> DragUpdated;

    private PlayerControls controls;

    private Camera mainCamera;

    private Coroutine draggingCoroutine;

    private bool _dragging = false;

    private void Awake()
    {
        controls = new PlayerControls();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
       controls.Enable(); 
    }

    private void OnDisable()
    {
       controls.Disable(); 
    }

    private void Start()
    {
        controls.Touch.PrimaryContact.performed += Drag;
        // controls.Touch.PrimaryContact.canceled += Drop;
    }

    public Vector2 PrimaryPosition()
    {
        return PositionUtils.ScreenToWorldPosition(mainCamera, controls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    private void Drag(InputAction.CallbackContext obj)
    {
        _dragging = !_dragging;
        Debug.Log("Drag Started");
        if (OnStartTouch != null) OnStartTouch(PositionUtils.ScreenToWorldPosition(mainCamera, 
            controls.Touch.PrimaryPosition.ReadValue<Vector2>()));

        if (_dragging)
        {
            Debug.Log("Dragging");
        }
        else
        {
            Debug.Log("Stop Dragging");
        }
    }

    private void Drop(InputAction.CallbackContext obj)
    {
        Debug.Log("Drop Fired");
        StopCoroutine(draggingCoroutine);
        if (OnEndTouch != null) OnEndTouch(PositionUtils.ScreenToWorldPosition(mainCamera, 
            controls.Touch.PrimaryPosition.ReadValue<Vector2>()));
    }

    private void Update()
    {
        if (!_dragging)
            return;
        Debug.Log(controls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }

    private IEnumerator DragUpdate()
    {
        while (_dragging)
        {
            Debug.Log(controls.Touch.PrimaryPosition.ReadValue<Vector2>());
            // DragUpdated?.Invoke(controls.Touch.PrimaryPosition.ReadValue<Vector2>());           
            yield return null;
        }
    }
}
