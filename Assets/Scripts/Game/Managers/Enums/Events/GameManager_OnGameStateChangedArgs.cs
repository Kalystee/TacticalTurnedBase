namespace PNN
{
    public class GameManager_OnGameStateChangedArgs
    {
        public readonly GameState gameState;

        public GameManager_OnGameStateChangedArgs(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}