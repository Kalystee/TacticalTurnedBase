using PNN.Battlegrounds.Areas;
using PNN.Battlegrounds.Turns;
using PNN.Entities;
using PNN.Entities.Events;
using PNN.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PNN.Battlegrounds
{
    public class BattlegroundController
    {
        public readonly Battleground battleground;
        private readonly BattlegroundView view;

        public BattlegroundController()
        {
            this.battleground = new Battleground();
            this.view = new GameObject("Battleground:View", typeof(BattlegroundView)).GetComponent<BattlegroundView>();
            
            Initialize();
        }

        public BattlegroundController(Battleground battleground, BattlegroundView view)
        {
            this.battleground = battleground;
            this.view = view;

            Initialize();
        }

        private void Initialize()
        {
            Find.GameManager.OnGamePlayStateChanged += HandleGamePlayStateChanged;

            this.battleground.OnEntitySpawned += this.HandleEntitySpawned;
            this.battleground.OnEntityDespawned += this.HandleEntityDespawned;
            this.battleground.OnTileChanged += this.view.OnTileChanged;

            Find.InputManager.OnMouseMoved += this.HandleOver;
            Find.InputManager.OnMouseClicked += this.HandleClick;
            Find.InputManager.OnKeyboardUsed += this.HandleKeyboard;

            this.view.CreateBlankTilemap(this.battleground.AllTiles);
        }

        public void Destroy()
        {
            this.view.Destroy();
        }

        #region Events
        private void HandleGamePlayStateChanged(object sender, GameManager_OnGamePlayStateChangedArgs args)
        {
            Debug.Log("Changed PlayState to " + args.gamePlayState);

            if (args.gamePlayState == GamePlayState.BEGIN_TURN)
            {
                HandlePreTurn();
                Find.GameManager.CurrentGamePlayState = GamePlayState.IDLE;
            }
            else if (args.gamePlayState == GamePlayState.SELECT_ABILITY)
            {
                Vector2Int playerPos = Find.Battleground.PlayingEntity.Position;
                var bc = Find.BattlegroundController;

                Area area = new Area_Distance(Find.AbilityBarView.SelectedAbility.range);
                bc.ResetEveryHighlights("abilityRange");
                bc.HighlightMultiples("abilityRange", ColorGame.ability, 500, area.GetAffectedArea(playerPos));
            }
            else if (args.gamePlayState == GamePlayState.SELECT_MOVEMENT)
            {
                Area area = new Area_Distance((int)Find.Battleground.PlayingEntity.Character.PA.CurrentValue);
                HighlightMultiples("Movement Possible", ColorGame.movement, 500, area.GetAffectedArea(Find.Battleground.PlayingEntity.Position, false));

                Debug.Log("Selectionner la case sur laquelle vous déplacer dessus !");
            }
            else
            {
                ResetEveryHighlights("Over");
            }
        }

        private void HandleEntitySpawned(object sender, Battleground_OnEntitySpawnedArgs args)
        {
            args.affectedEntity.OnDamageTaken +=
                delegate (object s, IDamageable_DamageTakenArgs e)
                {
                    if (e.HealthBeforeApplying.CurrentValue - e.DamageTaken < 0)
                    {
                        this.battleground.DespawnEntity(args.affectedEntity);
                    }
                };

            this.view.SpawnEntityGameObject(args.affectedEntity);
        }

        private void HandleEntityDespawned(object sender, Battleground_OnEntityDespawnedArgs args)
        {
            this.view.DespawnEntityGameObject(args.affectedEntity);
        }
        
        private void HandleOver(object sender, MouseMoveArgs args)
        {
            if (args.IsometricChanged)
            {
                if (Find.GameManager.CurrentGamePlayState == GamePlayState.SELECT_ABILITY)
                {
                    this.ResetEveryHighlights("Over");

                    AbilityBase abilityUsed = Find.AbilityBarView.SelectedAbility;
                    Vector2Int posClicked = args.worldIsometricPosition.AsVector2Int();
                    if (IsValidAbilityUsePosition(posClicked, abilityUsed))
                    {
                        
                        foreach(Vector2Int affectedTile in abilityUsed.area.GetAffectedArea(posClicked))
                        {
                            if (IsInsideBondaries(affectedTile))
                            {
                                this.GetHighlight(affectedTile)?.SetHighlight("Over", ColorGame.over, 1000);
                            }
                        }
                    }
                }
                else if (Find.GameManager.CurrentGamePlayState == GamePlayState.SELECT_MOVEMENT)
                {
                    GetHighlight(args.oldWorldIsometricPosition.AsVector2Int())?.TryRemoveHighlight("Over");
                    if (IsValidMovementPosition(args.worldIsometricPosition.AsVector2Int()))
                    { 
                        GetHighlight(args.worldIsometricPosition.AsVector2Int())?.SetHighlight("Over", ColorGame.over, 1000);
                    }
                }
                else
                {

                }
            }
        }
        
        private void HandleClick(object sender, MouseClickArgs args)
        {
            if (Find.GameManager.CurrentGamePlayState == GamePlayState.SELECT_MOVEMENT)
            {
                if (args.mouseClick == MouseButton.LeftClick)
                {
                    Vector2Int clickPos = args.worldIsometricPosition.AsVector2Int();
                    Entity entity = Find.Battleground.PlayingEntity;
                    if (IsValidMovementPosition(clickPos))
                    {
                        PlayAction(new TurnAction_Move(clickPos));
                    }
                    ResetEveryHighlights("Movement Possible");
                    Find.GameManager.CurrentGamePlayState = GamePlayState.IDLE;
                }
            }
            else if(Find.GameManager.CurrentGamePlayState == GamePlayState.SELECT_ABILITY)
            {
                if (args.mouseClick == MouseButton.LeftClick)
                {
                    Vector2Int clickPos = args.worldIsometricPosition.AsVector2Int();
                    AbilityBase abilityUsed = Find.AbilityBarView.SelectedAbility;
                    Entity caster = Find.Battleground.PlayingEntity;

                    if (IsValidAbilityUsePosition(clickPos,abilityUsed))
                    {
                        PlayAction(new TurnAction_Ability(this.battleground,clickPos,abilityUsed));
                    }

                    ResetEveryHighlights("abilityRange");
                    Find.AbilityBarView.SelectedAbility = null;
                    Find.GameManager.CurrentGamePlayState = GamePlayState.IDLE;
                }
            }
        }

        private void HandleKeyboard(object sender, KeyboardArgs args)
        {
            if (Find.GameManager.CurrentGamePlayState == GamePlayState.IDLE)
            {
                if (args.keyPresed == KeyCode.Space)
                {
                    if(Find.Battleground.PlayingEntity.Character.PA.CurrentValue > 0)
                        Find.GameManager.CurrentGamePlayState = GamePlayState.SELECT_MOVEMENT;
                }
            }
        }

        private bool IsInsideBondaries(Vector2Int position)
        {
            return this.battleground.AllTiles.Any(kvp => kvp.Key == position);
        }

        private bool IsValidAbilityUsePosition(Vector2Int positionClick, AbilityBase ability)
        {
            if (!IsInsideBondaries(positionClick))
                return false;

            if (ability == null)
                return false;

            return (positionClick - Find.Battleground.PlayingEntity.Position).Distance() <= ability.range && IsLineOfSightOpen(this.battleground.PlayingEntity.Position,positionClick);
        }

        private bool IsValidMovementPosition(Vector2Int position)
        {
            if (!IsInsideBondaries(position))
                return false;

            return (position - Find.Battleground.PlayingEntity.Position).Distance() <= Find.Battleground.PlayingEntity.Character.PA.CurrentValue;
        }
        #endregion

        public void PlayAction(TurnAction turnAction)
        {
            turnAction.PerformAction(Find.Battleground.PlayingEntity);
        }

        public void HighlightMultiples(string name, Color color, int priority, params Vector2Int[] positions)
        {
            if (positions != null)
            {
                foreach (Vector2Int vector2Int in positions)
                {
                    GetHighlight(vector2Int)?.SetHighlight(name, color, priority);
                }
            }
        }

        public void ResetMultiplesHighlight(params Vector2Int[] positions)
        {
            if (positions != null)
            {
                foreach (Vector2Int vector2Int in positions)
                {
                    GetHighlight(vector2Int).Reset();
                }
            }
        }

        public BattlegroundHighlight GetHighlight(Vector2Int position)
        {
            BattlegroundTile tile = battleground.GetTileAt(position);
            if (tile != null)
            {
                return tile.highlight;
            }
            
            return null;
        }

        public void ResetEveryHighlights(string named)
        {
            foreach (KeyValuePair<Vector2Int, BattlegroundTile> kvp in this.battleground.AllTiles)
            {
                kvp.Value.highlight.TryRemoveHighlight(named);
            }
        }

        public Entity GetEntityAt(Vector2Int vector2Int)
        {
            return battleground.AllEntities.ToList().Find(entity => entity.Position == vector2Int);
        }

        /// <summary>
        /// Method to get all the entities in an abilityArea
        /// </summary>
        /// <param name="positionImpact">Position where is used the ability</param>
        /// <param name="abilityUsed">ability use </param>
        /// <returns>List of all entities affect by the abilities</returns>
        public List<Entity> GetEntitiesAffectedByAbility(Vector2Int positionImpact, AbilityBase abilityUsed)
        {
            List<Entity> targets = new List<Entity>();
            foreach (Vector2Int position in abilityUsed.area.GetAffectedArea(positionImpact))
            {
                foreach (Entity entity in this.battleground.AllEntities)
                {
                    if (entity.Position.Equals(position))
                    {
                        targets.Add(entity);
                    }
                }
            }
            return targets;
        }

        public bool IsLineOfSightOpen(Vector2Int start, Vector2Int end)
        {
            Vector2 differences = new Vector2(start.x - start.x, start.y - start.y);

            int signX = (int)Mathf.Sign(differences.x);
            int signY = (int)Mathf.Sign(differences.y);

            float slope = differences.x / differences.y;
            if (differences.y == 0)
                slope = 0;

            float invSlope = differences.y / differences.x;
            if (differences.x == 0)
                invSlope = 0;

            if (Mathf.Abs(differences.x) > Mathf.Abs(differences.y))
            {
                for (int x = 0; x < Mathf.Abs(differences.x); x++)
                {
                    int X = signX * x;
                    int y1 = Mathf.FloorToInt(invSlope * X);
                    int y2 = Mathf.CeilToInt(invSlope * X);

                    BattlegroundTile t1 = this.battleground.GetTileAt(new Vector2Int(X, y1) + start);
                    if (t1 != null && t1.tile.isBlockingSight)
                    {
                        return false;
                    }

                    BattlegroundTile t2 = this.battleground.GetTileAt(new Vector2Int(X, y2) + start);
                    if (t2 != null && t2.tile.isBlockingSight)
                    {
                        return false;
                    }
                }
            }
            else if (Mathf.Abs(differences.x) < Mathf.Abs(differences.y))
            {
                for (int y = 0; y < Mathf.Abs(differences.y); y++)
                {
                    int Y = signY * y;
                    int x1 = Mathf.FloorToInt(slope * Y);
                    int x2 = Mathf.CeilToInt(slope * Y);

                    BattlegroundTile t1 = this.battleground.GetTileAt(new Vector2Int(x1, Y) + start);
                    if (t1 != null && t1.tile.isBlockingSight)
                    {
                        return false;
                    }

                    BattlegroundTile t2 = this.battleground.GetTileAt(new Vector2Int(x2, Y) + start);
                    if (t2 != null && t2.tile.isBlockingSight)
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < Mathf.Abs(differences.x); i++)
                {
                    BattlegroundTile t = this.battleground.GetTileAt(new Vector2Int(i * signX, i * signY) + start);
                    if (t != null && t.tile.isBlockingSight)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void NextTurn()
        {
            Find.GameManager.CurrentGamePlayState = GamePlayState.END_TURN;
            this.ReassignPlayingEntity();
            Find.GameManager.CurrentGamePlayState = GamePlayState.BEGIN_TURN;
        }

        public void HandlePreTurn()
        {
            //Do a bunch of Pre Stuff...
        }

        private void SortPlayingOrder<Tkey>(Func<Entity, Tkey> ordering)
        {
            this.battleground.PlayingOrder.Add(this.battleground.PlayingEntity);
            this.battleground.PlayingEntity = null;
            this.battleground.PlayingOrder.OrderBy(ordering);

            ReassignPlayingEntity();
        }

        private void ReassignPlayingEntity()
        {
            if (this.battleground.PlayingEntity != null)
                this.battleground.PlayingOrder.Add(this.battleground.PlayingEntity);
            this.battleground.PlayingEntity = this.battleground.PlayingOrder.First();
            this.battleground.PlayingOrder.RemoveAt(0);
        }
    }
}