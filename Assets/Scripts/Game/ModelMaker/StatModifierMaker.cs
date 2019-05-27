using PNN.Characters.Stats;
using Sirenix.OdinInspector;
using System;
using System.Text;
using UnityEngine;
using PNN.Enums;

namespace PNN.Makers
{
    [Serializable, InlineProperty]
    public struct StatModifierMaker
    {
        [SerializeField] public StatType StatInfluenced;
        [SerializeField] private float Value;
        [SerializeField] private StatModType ModType;
        [SerializeField] private bool OverrideOrder;
        [SerializeField, ShowIf("OverrideOrder") ,InfoBox("Classic Order (Low value comes first) : Flat (100), PercentAdd(200), PercentMulti(300)")] private int CustomOrder;

        public StatModifier CreateStatModifier(object source)
        {
            if (OverrideOrder)
                return new StatModifier(Value, ModType, CustomOrder, source);
            else
                return new StatModifier(Value, ModType, source);
        }

        public override string ToString()
        {
            if(ModType == StatModType.Flat)
            {
                StringBuilder builder = new StringBuilder("+ ");
                builder.Append(Math.Round(Value, 2));
                return builder.ToString();
            }
            else if(ModType == StatModType.PercentAdd)
            {
                StringBuilder builder = new StringBuilder("+ ");
                builder.Append(Math.Round(Value, 2));
                builder.Append(" %");
                return builder.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder("x ");
                builder.Append(Math.Round(Value, 2));
                builder.Append(" %");
                return builder.ToString();
            }
        }
    }
}
