using UnityEngine;
using PNN.Battlegrounds;
using PNN.Entities;
using System;
using Sirenix.OdinInspector;

namespace PNN.Makers
{
    [Serializable]
    public class BattlegroundMaker : MonoBehaviour
    {
        [BoxGroup("Battleground"), SerializeField] private Vector2Int size;
        [BoxGroup("Battleground"), SerializeField] private GameTile basicGameTile;

        private void Awake()
        {
            CreateBattleground();
        }

        private void CreateBattleground()
        {
            Find.GameManager.CurrentGameState = GameState.PLAYING;
            
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    Find.Battleground.SetTile(new Vector2Int(x, y), new BattlegroundTile(basicGameTile, new BattlegroundHighlight()));
                }
            }

            SpawnAnEntity();
        }

        private void Start()
        {
            Find.BattlegroundController.NextTurn();
        }

        [BoxGroup("Entities"), SerializeField] private CharacterMaker testCharacter;
        [BoxGroup("Entities"), SerializeField] private Vector2Int testEntityPos;
        [BoxGroup("Entities"), SerializeField] private Team testTeam;

        [Button("Spawn Entity")]
        private void SpawnAnEntity()
        {
            if (testTeam != null)
            {
                Entity entity = new Entity(Find.Battleground, testCharacter.CreateCharacter(), testEntityPos, testTeam);
                Find.Battleground.SpawnEntity(entity);
            }
        }

        [Button("Next Turn")]
        private void NextTurn()
        {
            Find.BattlegroundController.NextTurn();
        }
    }
}