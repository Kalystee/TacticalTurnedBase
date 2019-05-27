using System;
using System.Collections.Generic;
using System.Linq;

namespace PNN.Characters.Abilities
{
    public class AbilityKnowledge
    {
        public event EventHandler<AbilityKnowledge_OnChangeArgs> OnChangeArgs;
        readonly List<LearntAbility> learntAbilitiesList;

        private bool isDirty;

        private AbilityBase[] _learnAbilities;
        public AbilityBase[] LearntAbilities
        {
            get
            {
                if(isDirty)
                {
                    _learnAbilities = learntAbilitiesList.Select(la => la.ability).ToArray();
                }
                return _learnAbilities;
            }
        }

        public AbilityKnowledge()
        {
            learntAbilitiesList = new List<LearntAbility>();
        }

        public bool AlreadyKnowThisAbility(AbilityBase ability)
        {
            return LearntAbilities.Contains(ability);
        }

        public bool AlreadyKnowThisAbilityFrom(object source, AbilityBase ability)
        {
            return learntAbilitiesList.Any(la => la.source == source && la.ability == ability);
        }

        public void LearnAbilities(params AbilityBase[] abilities)
        {
            LearnAbilitiesFromSource(null, abilities);
        }

        public void LearnAbilitiesFromSource(object source, params AbilityBase[] abilities)
        {
            if (abilities == null)
                return;

            bool hasLearnt = false;
            List<AbilityBase> newAbilities = new List<AbilityBase>();

            foreach (AbilityBase ability in abilities)
            {
                if (LearnAbility(source, ability))
                {
                    hasLearnt = true;
                    newAbilities.Add(ability);
                }
            }

            if (hasLearnt)
            {
                isDirty = true;
                AbilityKnowledge_OnChangeArgs args = new AbilityKnowledge_OnChangeArgs(LearntAbilities, newAbilities.ToArray(), true);
                OnChangeArgs?.Invoke(this, args);
            }
        }

        protected bool LearnAbility(object source, AbilityBase ability)
        {
            if (ability == null)
                return false;

            if (!AlreadyKnowThisAbilityFrom(source, ability))
            {
                learntAbilitiesList.Add(new LearntAbility(source, ability));
                return true;
            }

            return false;
        }

        public void ForgotAbilityFromSource(object source)
        {
            learntAbilitiesList.RemoveAll(learntAbility => learntAbility.source == source);
        }

        public void ForgotAbilities(params AbilityBase[] abilities)
        {
            if (abilities == null)
                return;

            bool hasForgot = false;
            List<AbilityBase> removedAbilities = new List<AbilityBase>();

            foreach (AbilityBase ability in abilities)
            {
                if (ForgotAbility(ability))
                {
                    hasForgot = true;
                    removedAbilities.Add(ability);
                }
            }

            if (hasForgot)
            {
                isDirty = true;
                AbilityKnowledge_OnChangeArgs args = new AbilityKnowledge_OnChangeArgs(LearntAbilities, removedAbilities.ToArray(), false);
                OnChangeArgs?.Invoke(this, args);
            }
        }

        protected bool ForgotAbility(AbilityBase ability)
        {
            if (ability == null)
                return false;

            if (!AlreadyKnowThisAbilityFrom(null, ability))
            {
                return learntAbilitiesList.Remove(learntAbilitiesList.Find(la => la.source == null && la.ability == ability));
            }

            return false;
        }
    }
}