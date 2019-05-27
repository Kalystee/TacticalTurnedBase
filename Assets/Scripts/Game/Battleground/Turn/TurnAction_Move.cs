using PNN.Enums;
using PNN.Entities;
using UnityEngine;

namespace PNN.Battlegrounds.Turns
{
    public class TurnAction_Move : TurnAction
    {
        readonly Vector2Int targetPosition;
        readonly MoveType moveType;

        public TurnAction_Move(Vector2Int targetPosition, MoveType moveType = MoveType.WALKING)
        {
            this.targetPosition = targetPosition;
            this.moveType = moveType;
        }

        public override bool PerformAction(Entity entity)
        {
            switch (moveType)
            {
                case MoveType.WALKING:
                    MoveByWalking(entity);
                    break;

                case MoveType.FLYING:
                    MoveByAir(entity);
                    break;

                case MoveType.TELEPORTING:
                    MoveByTeleport(entity);
                    break;

                default:
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Déplacement a pieds (Obstacles qui gènent)
        /// </summary>
        /// <param name="entity">Entité affecté</param>
        private void MoveByWalking(Entity entity)
        {
            entity.Character.PA.CurrentValue -= (targetPosition - entity.Position).Distance();
            entity.Position = targetPosition;

            //Todo : Bouger l'entité selon un pathfinding afin de garder le meilleur chemin
        }

        /// <summary>
        /// Déplacement par les airs (Obstacles ne gènent pas)
        /// </summary>
        /// <param name="entity">Entité affecté</param>
        private void MoveByAir(Entity entity)
        {
            entity.Character.PA.CurrentValue -= (targetPosition - entity.Position).Distance();
            entity.Position = targetPosition;

            //Todo : Bouger l'entité selon un pathfinding en ignorant certains obstacles
        }

        /// <summary>
        /// Déplacement instantanné
        /// </summary>
        /// <param name="entity">Entité affecté</param>
        private void MoveByTeleport(Entity entity)
        {
            entity.Character.PA.CurrentValue -= (targetPosition - entity.Position).Distance();
            entity.Position = targetPosition;
        }

        public override string ToString()
        {
            switch (moveType)
            {
                case MoveType.WALKING:
                    return $"Déplacement vers {targetPosition}";

                case MoveType.TELEPORTING:
                    return $"Téléportation en {targetPosition}";

                case MoveType.FLYING:
                    return $"Vol vers {targetPosition}";
            }

            return "[Type de déplacement inconnu]";
        }
    }
}
