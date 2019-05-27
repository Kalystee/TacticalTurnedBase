namespace PNN
{
    public class GameManager_OnGamePlayStateChangedArgs
    {
        public readonly GamePlayState gamePlayState;

        public GameManager_OnGamePlayStateChangedArgs(GamePlayState gamePlayState)
        {
            this.gamePlayState = gamePlayState;
        }
    }
}