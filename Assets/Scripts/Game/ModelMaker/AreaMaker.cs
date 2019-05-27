using PNN.Battlegrounds.Areas;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PNN.Makers
{
    [Serializable, InlineProperty]
    public class AreaMaker
    {
        [SerializeField, TitleGroup("Ability Area Maker")] private AreaType areaType;
        [SerializeField, ShowIf("ShouldDisplayRadius"), TitleGroup("Advanced Settings"), MinValue(0)] private int radius;
        [SerializeField, ShowIf("ShouldDisplayAreaCrossType"), TitleGroup("Advanced Settings")] private Area_Cross.CrossType crossType;
        
        private bool ShouldDisplayRadius()
        {
            return areaType != AreaType.POINT;
        }

        private bool ShouldDisplayAreaCrossType()
        {
            return areaType == AreaType.CROSS;
        }

        public Area CreateAbilityArea()
        {
            if(areaType == AreaType.POINT)
            {
                return new Area_Point();
            }
            else if(areaType == AreaType.ROUND)
            {
                return new Area_Round(radius);
            }
            else if (areaType == AreaType.DISTANCE)
            {
                return new Area_Distance(radius);
            }
            else
            {
                return new Area_Cross(radius, crossType);
            }
        }
    }

    public enum AreaType
    {
        POINT,
        ROUND,
        DISTANCE,
        CROSS
    }
}
