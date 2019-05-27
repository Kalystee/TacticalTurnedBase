namespace PNN.Characters.Abilities
{
    public class AbilityKnowledge_OnChangeArgs
    {
        public readonly AbilityBase[] abilities;
        public readonly AbilityBase[] changedAbilities;
        public readonly bool hasLearnt;

        public AbilityKnowledge_OnChangeArgs(AbilityBase[] abilities, AbilityBase[] changedAbilities, bool hasLearnt)
        {
            this.abilities = abilities;
            this.changedAbilities = changedAbilities;
            this.hasLearnt = hasLearnt;
        }
    }
}
