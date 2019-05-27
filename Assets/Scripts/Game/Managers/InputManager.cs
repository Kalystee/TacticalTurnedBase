using PNN;
using PNN.Enums;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    public EventHandler<MouseClickArgs> OnMouseClicked;
    public EventHandler<MouseMoveArgs> OnMouseMoved;
    public EventHandler<KeyboardArgs> OnKeyboardUsed;

    private Vector3 oldMousePosition;
    private Vector3Int isoPos;

    private Vector3 MousePosition
    {
        get
        {
            return oldMousePosition;
        }
        set
        {
            if(oldMousePosition != value)
            {
                Vector3Int newIsoPos = Find.Camera.ScreenToWorldPoint(Input.mousePosition).IsometricToCarthesian().AsVector3IntCeil();
                newIsoPos.z = 0;

                MouseMoveArgs args = new MouseMoveArgs(value, oldMousePosition, newIsoPos, isoPos);
                OnMouseMoved?.Invoke(this, args);

                this.oldMousePosition = value;
                this.isoPos = newIsoPos;
            }
        }
    }

    private MouseButton buttonsUsed;
    private MouseButton Buttons
    {
        get
        {
            return buttonsUsed;
        }
        set
        {
            if(buttonsUsed != value)
            {
                MouseClickArgs args = new MouseClickArgs(isoPos, value, buttonsUsed, EventSystem.current.IsPointerOverGameObject());
                OnMouseClicked?.Invoke(this, args);

                this.buttonsUsed = value;
            }
        }
    }

    private void Awake()
    {
        oldMousePosition = Input.mousePosition;
    }

    private void Update()
    {
        HandleMouseButton();
        HandleMousePosition();
        HandleKeyboard();
    }

    private void HandleMousePosition()
    {
        if (oldMousePosition != Input.mousePosition)
        {
            MousePosition = Input.mousePosition;
        }
    }

    private void HandleMouseButton()
    {
        MouseButton button = MouseButton.None;
        if (Input.GetMouseButton(0))
        {
            button |= MouseButton.LeftClick;
        }
        if (Input.GetMouseButton(1))
        {
            button |= MouseButton.RightClick;
        }
        if (Input.GetMouseButton(2))
        {
            button |= MouseButton.MiddleClick;
        }
        Buttons = button;
    }

    private void HandleKeyboard()
    {
        foreach (KeyCode key in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key))
            {
                KeyboardArgs args = new KeyboardArgs(key);
                OnKeyboardUsed?.Invoke(this, args);
            }
        }
    }
}
