using PNN.Entities.Events;
using System;

namespace PNN.Entities.Interfaces
{
    public interface IOverable
    {
        event EventHandler<IOverable_OnOverArgs> OnOvered;
        event EventHandler<IOverable_OnOverArgs> OnUnovered;
    }
}