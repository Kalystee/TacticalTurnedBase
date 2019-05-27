using UnityEngine;

namespace PNN.Enums
{
    public enum Direction
    {
        North,
        East,
        West,
        South
    }

    public static class DirectionExtension
    {
        /// <summary>
        /// Obtenir une direction selon 2 positions voisines
        /// </summary>
        /// <param name="t1">Vecteur 1</param>
        /// <param name="t2">Vecteur 2</param>
        /// <returns>Direction du Vecteur 1 au Vecteur 2</returns>
        public static Direction GetDirection(this Vector2Int t1, Vector2Int t2)
        {
            if (t1.y < t2.y)
                return Direction.North;
            if (t1.x < t2.x)
                return Direction.East;
            if (t1.y > t2.y)
                return Direction.South;
            return Direction.West;
        }

        /// <summary>
        /// Traduit une direction en vecteur
        /// </summary>
        /// <param name="direction">Direction cible</param>
        /// <returns>Vecteur correspondant</returns>
        public static Vector2 ToEuler(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return Vector2.right;
                case Direction.North:
                    return Vector2.up;
                case Direction.West:
                    return Vector2.left;
                default:
                    return Vector2.down;
            }
        }

        /// <summary>
        /// Traduit une direction en vecteur entier
        /// </summary>
        /// <param name="direction">Direction cible</param>
        /// <returns>Vecteur entier correspondant</returns>
        public static Vector2Int ToEulerInt(this Direction direction)
        {
            switch (direction)
            {
                case Direction.East:
                    return Vector2Int.right;
                case Direction.North:
                    return Vector2Int.up;
                case Direction.West:
                    return Vector2Int.left;
                default:
                    return Vector2Int.down;
            }
        }
    }
}
