using PNN.Entities.Events;
using System;

namespace PNN.Entities.Interfaces
{
    public interface IClickable
    {
        event EventHandler<IClickable_OnClickArgs> OnClicked;
    }
}