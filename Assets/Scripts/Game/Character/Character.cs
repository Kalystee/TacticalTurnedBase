using System;
using PNN.Enums;
using PNN.Characters.Stats;
using PNN.Characters.Items;
using PNN.Characters.Abilities;
using System.Collections.Generic;

namespace PNN.Characters
{
    [Serializable]
    public class Character
    {
        public readonly string name;
        
        #region Stats
        readonly Dictionary<StatType, CharacterStat> stats;

        public IReadOnlyDictionary<StatType, CharacterStat> Stats
        {
            get { return stats; }
        }

        public CharacterStat GetStat(StatType type)
        {
            return stats[type];
        }
        public CharacterStatVariable PV
        {
            get { return GetStat(StatType.PV) as CharacterStatVariable; }
        }

        public CharacterStatVariable PA
        {
            get { return GetStat(StatType.PA) as CharacterStatVariable; }
        }

        public CharacterStat Strength
        {
            get { return GetStat(StatType.Force); }
        }

        public CharacterStat Dexterity
        {
            get { return GetStat(StatType.Dexterite); }
        }

        public CharacterStat Consitution
        {
            get { return GetStat(StatType.Constitution); }
        }

        public CharacterStat Intelligence
        {
            get { return GetStat(StatType.Intelligence); }
        }
        #endregion

        //Inventory & Abilities
        public readonly Inventory inventory;
        public readonly Gear gear;
        public readonly AbilityKnowledge abilityKnowledge;

        //Constructor
        public Character(string name, int pvValue, int paValue, int strengthValue, int dexterityValue, int constitutionValue, int intelligenceValue)
        {
            this.name = name;

            this.stats = new Dictionary<StatType, CharacterStat>
            {
                //Ajout de toute les stats
                { StatType.PV,              new CharacterStatVariable(pvValue) },
                { StatType.PA,              new CharacterStatVariable(paValue) },
                { StatType.Force,           new CharacterStat(strengthValue) },
                { StatType.Dexterite,       new CharacterStat(dexterityValue) },
                { StatType.Constitution,    new CharacterStat(constitutionValue) },
                { StatType.Intelligence,    new CharacterStat(intelligenceValue) }
            };

            this.inventory = new Inventory();
            this.gear = new Gear();
            this.abilityKnowledge = new AbilityKnowledge();
        }
    }
}


