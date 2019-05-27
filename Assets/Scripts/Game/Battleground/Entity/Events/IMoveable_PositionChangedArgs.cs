using UnityEngine;

namespace PNN.Entities.Events
{
    public class IMoveable_PositionChangedArgs
    {
        public readonly Vector2Int OldPosition;
        public readonly Vector2Int NextPosition;

        public IMoveable_PositionChangedArgs(Vector2Int oldPosition, Vector2Int nextPosition)
        {
            this.OldPosition = oldPosition;
            this.NextPosition = nextPosition;
        }
    }
}
