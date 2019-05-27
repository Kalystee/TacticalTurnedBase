using System;

namespace PNN.Enums
{
    [Flags]
    public enum MouseButton
    {
        None = 0,
        LeftClick = 1,
        RightClick = 2,
        MiddleClick = 4
    }
}