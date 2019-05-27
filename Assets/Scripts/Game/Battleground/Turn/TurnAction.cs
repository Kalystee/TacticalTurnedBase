using PNN.Entities;

namespace PNN.Battlegrounds.Turns
{
    public abstract class TurnAction
    {
        /// <summary>
        /// Action effectué
        /// </summary>
        /// <param name="entity">Entité affecté</param>
        /// <returns>True si l'action est valide</returns>
        public abstract bool PerformAction(Entity entity);
    }
}