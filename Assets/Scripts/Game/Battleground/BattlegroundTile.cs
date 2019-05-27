namespace PNN.Battlegrounds
{
    public class BattlegroundTile
    {
        public readonly GameTile tile;
        public readonly BattlegroundHighlight highlight;

        public BattlegroundTile(GameTile tile, BattlegroundHighlight highlight)
        {
            this.tile = tile;
            this.highlight = highlight;
        }
    }
}