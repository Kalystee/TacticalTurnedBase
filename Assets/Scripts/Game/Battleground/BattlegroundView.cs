using System.Collections.Generic;
using UnityEngine;
using PNN.Entities;
using UnityEngine.Tilemaps;

namespace PNN.Battlegrounds
{
    public class BattlegroundView : MonoBehaviour
    {
        public Tilemap Tilemap { get; private set; }
        private Transform entitiesParent;

        public void Destroy()
        {
            Destroy(this.gameObject);
        }

        public void OnTileChanged(object sender, Battleground_OnTileChangedArgs args)
        {
            ChangeTile(args.position, args.newTile);

            if(args.newTile == null)
            {
                RemoveTile(args.position);
            }
            else
            {
                ChangeTile(args.position, args.newTile);
                if (args.wasNull)
                {
                    CreateTileHighlight(args.position, args.newTile);
                }
            }
        }
        
        public void CreateBlankTilemap(IReadOnlyDictionary<Vector2Int, BattlegroundTile> tiles)
        {
            if (Tilemap == null)
            {
                GameObject tileGameObject = new GameObject("Battleground:Tilemap", typeof(Tilemap), typeof(Grid), typeof(TilemapRenderer));
                Tilemap = tileGameObject.GetComponent<Tilemap>();

                Grid grid = tileGameObject.GetComponent<Grid>();
                grid.cellLayout = GridLayout.CellLayout.Isometric;
                grid.cellSize = new Vector3(1f, .5f, 1f);

                TilemapRenderer renderer = tileGameObject.GetComponent<TilemapRenderer>();
                renderer.sortOrder = TilemapRenderer.SortOrder.TopRight;

                tileGameObject.transform.SetParent(this.transform);
                tileGameObject.transform.position = new Vector3(0, -.75f, 0);

                foreach (KeyValuePair<Vector2Int, BattlegroundTile> tileValue in tiles)
                {
                    ChangeTile(tileValue.Key, tileValue.Value);
                    CreateTileHighlight(tileValue.Key, tileValue.Value);
                }

                entitiesParent = new GameObject("Entities:GameObjects").transform;
            }
        }

        private void CreateTileHighlight(Vector2Int vector2Int, BattlegroundTile battlegroundTile)
        {
            Vector3Int vector3Int = vector2Int.AsVector3Int() + new Vector3Int(0,0,1);
            Tilemap.SetTile(vector3Int, Find.Prefabs.highlightTile);
            Tilemap.SetTileFlags(vector3Int, TileFlags.None);

            Tilemap.SetColor(vector3Int, battlegroundTile.highlight.GetColor());

            battlegroundTile.highlight.OnColorChanged += delegate (object sender, Battleground_OnHighlightChangedArgs args)
            {
                Tilemap.SetColor(vector3Int, args.color);
            };
        }

        private void ChangeTile(Vector2Int vector2Int, BattlegroundTile battlegroundTile)
        {
            Vector3Int vector3Int = vector2Int.AsVector3Int();
            Tilemap.SetTile(vector3Int, battlegroundTile.tile.tile2D);
            Tilemap.SetTileFlags(vector3Int, TileFlags.None);
        }

        private void RemoveTile(Vector2Int vector2Int)
        {
            Vector3Int vector3Int = vector2Int.AsVector3Int();
            Tilemap.SetTile(vector3Int, null);
            Tilemap.SetTile(vector3Int + new Vector3Int(0,0,1), null);
        }
        
        List<EntityController> entityControllers = new List<EntityController>();
        Dictionary<Entity, GameObject> entityGameObjectsDictionary = new Dictionary<Entity, GameObject>();

        public void SpawnEntityGameObject(Entity entity)
        {
            GameObject entityGameObject = Instantiate(Find.Prefabs.entityPrefab);
            entityGameObject.transform.SetParent(entitiesParent);

            SpriteRenderer renderer = entityGameObject.GetComponent<SpriteRenderer>();
            renderer.color = entity.Team.teamColor;

            EntityView entityView = entityGameObject.GetComponent<EntityView>();

            EntityController entityController = new EntityController(entity, entityView);
            entityControllers.Add(entityController);

            entityGameObjectsDictionary.Add(entity, entityGameObject);
        }

        public void DespawnEntityGameObject(Entity entity)
        {
            GameObject entityGameObject = entityGameObjectsDictionary[entity];
            /*
                Do bunch of animation stuff;
            */
            Destroy(entityGameObject);

            EntityController entityController = entityControllers.Find(ec => ec.entity == entity);
            entityControllers.Remove(entityController);
        }

        public EntityController GetEntityController(Entity entity)
        {
            return entityControllers.Find(ec => ec.entity == entity);
        }
    }
}
