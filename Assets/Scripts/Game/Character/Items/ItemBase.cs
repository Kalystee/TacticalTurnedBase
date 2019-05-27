using Sirenix.OdinInspector;
using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    /// <summary>
    /// Price of the item
    /// </summary>
    [BoxGroup("General Settings")] public int Price;

    /// <summary>
    /// Limit of stack of the Item
    /// </summary>
    [BoxGroup("General Settings")] public int StackLimit;

    /// <summary>
    /// Sprite of the Item
    /// </summary>
    [BoxGroup("General Settings")] public Sprite Sprite;
}