using PNN.Characters;
using PNN.Characters.Stats;
using PNN.Enums;
using PNN.Makers;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;

public abstract class EquipableBase : ItemBase
{
    [BoxGroup("Equipment Settings")]
    public List<StatModifierMaker> statList;
    
    [BoxGroup("Equipment Settings")]
    public AbilityBase[] abilities;

    public void Equip(Character character)
    {
        foreach(StatModifierMaker smm in statList)
        {
            StatModifier modifier = smm.CreateStatModifier(this);
            character.GetStat(smm.StatInfluenced);
        }

        character.abilityKnowledge.LearnAbilitiesFromSource(this, abilities);
    }

    public void Unequip(Character character)
    {
        foreach (StatType st in statList.Select(smm => smm.StatInfluenced).Distinct())
        {
            character.GetStat(st).RemoveAllModifiersFromSource(this);
        }

        character.abilityKnowledge.ForgotAbilityFromSource(this);
    }
}

