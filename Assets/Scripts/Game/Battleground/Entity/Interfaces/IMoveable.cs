using System;
using PNN.Entities.Events;
using UnityEngine;

namespace PNN.Entities.Interfaces
{
    public interface IMoveable
    {
        Vector2Int Position { get; set; }

        event EventHandler<IMoveable_PositionChangedArgs> OnPositionChanged;
    }
}
