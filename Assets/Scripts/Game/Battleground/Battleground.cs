using PNN.Entities;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace PNN.Battlegrounds
{
    public class Battleground
    {
        public event EventHandler<Battleground_OnEntitySpawnedArgs> OnEntitySpawned;
        public event EventHandler<Battleground_OnEntityDespawnedArgs> OnEntityDespawned;
        public event EventHandler<Battleground_OnTileChangedArgs> OnTileChanged;

        readonly Dictionary<Vector2Int, BattlegroundTile> tiles = new Dictionary<Vector2Int, BattlegroundTile>();
        public IReadOnlyDictionary<Vector2Int, BattlegroundTile> AllTiles { get { return tiles; } }

        public void SetTile(Vector2Int vector2Int, BattlegroundTile gameTile)
        {
            if (vector2Int.x < 0 || vector2Int.y < 0)
                return;

            bool alreadyPlaced = this.tiles.ContainsKey(vector2Int);

            if (alreadyPlaced)
            {
                if (gameTile == null)
                    this.tiles.Remove(vector2Int);
                this.tiles[vector2Int] = gameTile;
            }
            else
            {
                this.tiles.Add(vector2Int, gameTile);
            }

            Battleground_OnTileChangedArgs args = new Battleground_OnTileChangedArgs(vector2Int, gameTile, !alreadyPlaced);
            OnTileChanged?.Invoke(this, args);
        }
        public BattlegroundTile GetTileAt(Vector2Int vector2Int)
        {
            if (vector2Int.x < 0 || vector2Int.y < 0)
                return null;

            if (tiles.ContainsKey(vector2Int))
            {
                return tiles[vector2Int];
            }

            return null;
        }

        readonly List<Entity> entities = new List<Entity>();
        public IReadOnlyCollection<Entity> AllEntities { get { return entities.AsReadOnly(); } }

        public bool SpawnEntity(Entity entity)
        {
            if (this.entities.Contains(entity) || entities.Any(e => e.Position == entity.Position))
                return false;
            
            this.entities.Add(entity);

            playingOrder.Add(entity);

            Battleground_OnEntitySpawnedArgs args = new Battleground_OnEntitySpawnedArgs(entity);
            OnEntitySpawned?.Invoke(this, args);

            return true;
        }

        public bool DespawnEntity(Entity entity)
        {
            if (!this.entities.Contains(entity))
                return false;

            this.entities.Remove(entity);

            playingOrder.Remove(entity);

            Battleground_OnEntityDespawnedArgs args = new Battleground_OnEntityDespawnedArgs(entity);
            OnEntityDespawned?.Invoke(this, args);

            return true;
        }

        private Entity playingEntity;
        public Entity PlayingEntity
        {
            get
            {
                return playingEntity;
            }
            set
            {
                playingEntity = value;
            }
        }

        readonly List<Entity> playingOrder = new List<Entity>();
        public List<Entity> PlayingOrder
        {
            get
            {
                return playingOrder;
            }
        }
        
    }
}
