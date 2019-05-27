using PNN.Entities;
using UnityEngine;

namespace PNN.Battlegrounds.Turns
{
    public class TurnAction_Ability : TurnAction
    {
        readonly Vector2Int targetPosition;
        readonly AbilityBase abilityUsed;

        public TurnAction_Ability(Battleground battleground, Vector2Int targetPosition, AbilityBase abilityUsed)
        {
            this.targetPosition = targetPosition;
            this.abilityUsed = abilityUsed;
        }

        public override bool PerformAction(Entity entity)
        {
            return this.abilityUsed.Use(Find.BattlegroundController, entity, targetPosition);
        }
    }
}
