using PNN.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class GameTile : ScriptableObject
{
    [BoxGroup("Visual")]
    public Tile tile2D;

    [BoxGroup("General Settings")]
    public bool isWalkable;
    [BoxGroup("General Settings")]
    public bool isBlockingSight;

    /// <summary>
    /// Effet lorsqu'un entité passe sur le GameTile
    /// </summary>
    /// <param name="entity">Entité affecté</param>
    public abstract void EffectEntity(Entity entity);
}
