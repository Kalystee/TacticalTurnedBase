using System;
using UnityEngine;

namespace PNN.Battlegrounds.Areas
{
    [Serializable]
    public abstract class Area
    {
        /// <summary>
        /// Regarde si la position est dans la zone
        /// </summary>
        /// <param name="position">Position</param>
        /// <returns></returns>
        public abstract bool IsInside(Vector2Int position);
        
        /// <summary>
        /// Retourne toutes les positions dans la zone
        /// </summary>
        /// <param name="offset">Position centrale</param>
        /// <param name="incluedCenter">Inclure le centre ou non</param>
        /// <returns>Positions (non-relatives)</returns>
        public abstract Vector2Int[] GetAffectedArea(Vector2Int offset, bool incluedCenter = true);
    }
}
