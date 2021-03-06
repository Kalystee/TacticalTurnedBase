﻿using System;

namespace PNN.Characters.Stats
{
    public enum StatModType
    {
        Flat = 100,
        PercentAdd = 200,
        PercentMulti = 300,
    }

    [Serializable]
    public class StatModifier
    {
        public readonly float Value;
        public readonly StatModType Type;
        public readonly int Order;
        public readonly object Source;

        public StatModifier(float value, StatModType type, int order, object source)
        {
            this.Value = value;
            this.Type = type;
            this.Order = order;
            this.Source = source;
        }

        public StatModifier(float value, StatModType type) : this (value, type, (int)type, null) { }

        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }

        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
    }
}
