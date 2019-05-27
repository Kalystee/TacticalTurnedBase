using PNN.Entities.Events;
using System;

namespace PNN.Entities.Interfaces
{
    public interface IDamageable
    {
        event EventHandler<IDamageable_DamageTakenArgs> OnDamageTaken;

        void TakeDamage(int damage);
    }
}