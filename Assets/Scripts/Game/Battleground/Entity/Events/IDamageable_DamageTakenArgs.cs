using PNN.Characters.Stats;

namespace PNN.Entities.Events
{
    public class IDamageable_DamageTakenArgs
    {
        public readonly CharacterStatVariable HealthBeforeApplying;
        public readonly int DamageTaken;

        public IDamageable_DamageTakenArgs(CharacterStatVariable healthBeforeApplying, int damageTaken)
        {
            this.HealthBeforeApplying = healthBeforeApplying;
            this.DamageTaken = damageTaken;
        }
    }
}