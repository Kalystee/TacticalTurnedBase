using PNN.Entities.Events;

namespace PNN.Entities
{
    public class EntityController
    {
        public readonly Entity entity;
        private readonly EntityView view;

        public EntityController(Entity entity, EntityView view)
        {
            this.entity = entity;
            this.view = view;

            this.entity.OnPositionChanged += HandlePositionChanged;

            this.view.OnClicked += HandleClicked;

            SyncPositions();
        }

        #region Events
        private void HandleClicked(object sender, IClickable_OnClickArgs e)
        {

        }

        private void HandlePositionChanged(object sender, IMoveable_PositionChangedArgs e)
        {
            SyncPositions();
        }

        #endregion

        /// <summary>
        /// Synchronise les positions entre la vue et le modèle
        /// </summary>
        private void SyncPositions()
        {
            this.view.transform.position = this.entity.Position.AsVector2().CarthesianToIsometric();
        }
    }
}