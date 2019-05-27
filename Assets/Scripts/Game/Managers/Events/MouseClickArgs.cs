using PNN.Enums;
using UnityEngine;

public class MouseClickArgs
{
    public readonly Vector3Int worldIsometricPosition;
    public readonly MouseButton mouseClick;
    public readonly MouseButton oldMouseClick;
    public readonly bool isOverUI;
    
    public MouseClickArgs(Vector3Int worldIsometricPosition, MouseButton mouseClick, MouseButton oldMouseClick, bool isOverUI)
    {
        this.worldIsometricPosition = worldIsometricPosition;
        this.mouseClick = mouseClick;
        this.oldMouseClick = oldMouseClick;
        this.isOverUI = isOverUI;
    }

    public bool IsLeftClickDown()
    {
        return (oldMouseClick & MouseButton.LeftClick) != MouseButton.LeftClick
            && (mouseClick & MouseButton.LeftClick) == MouseButton.LeftClick;
    }

    public bool IsRightClickDown()
    {
        return (oldMouseClick & MouseButton.RightClick) != MouseButton.RightClick
            && (mouseClick & MouseButton.RightClick) == MouseButton.RightClick;
    }

    public bool IsMiddleClickDown()
    {
        return (oldMouseClick & MouseButton.MiddleClick) != MouseButton.MiddleClick
            && (mouseClick & MouseButton.MiddleClick) == MouseButton.MiddleClick;
    }

    public bool IsLeftClickUp()
    {
        return (oldMouseClick & MouseButton.LeftClick) != MouseButton.LeftClick
            && (mouseClick & MouseButton.LeftClick) == MouseButton.LeftClick;
    }

    public bool IsRightClickUp()
    {
        return (oldMouseClick & MouseButton.RightClick) == MouseButton.RightClick
            && (mouseClick & MouseButton.RightClick) != MouseButton.RightClick;
    }

    public bool IsMiddleClickUp()
    {
        return (oldMouseClick & MouseButton.MiddleClick) == MouseButton.MiddleClick
            && (mouseClick & MouseButton.MiddleClick) != MouseButton.MiddleClick;
    }
}
