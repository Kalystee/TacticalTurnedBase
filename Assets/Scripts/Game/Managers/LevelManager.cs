using PNN.Battlegrounds;
using UnityEngine;

namespace PNN
{
    public class LevelManager : MonoBehaviour
    {
        private void Awake()
        {
            Find.GameManager.OnGameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(object sender, GameManager_OnGameStateChangedArgs args)
        {
            if(args.gameState == GameState.PLAYING)
            {
                CreateLevel();
            }
            else
            {
                if(Find.Battleground != null)
                {
                    Find.BattlegroundController.Destroy();
                    Find.GameManager.battlegroundController = null;
                }
            }
        }

        private void CreateLevel()
        {
            Find.GameManager.battlegroundController = new BattlegroundController();
        }
    }
}
