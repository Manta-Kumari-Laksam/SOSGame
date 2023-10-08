using SOSGame.Interface;

namespace SOSGame
{
    public class GameState
    {
        #region Public Properties
        public int boardSize { get; set; }
        public Board board{ get; set; }
        public AbstractPlayer player1 { get; set; }
        public AbstractPlayer player2 { get; set; }
        public int currentPlayer { get; set; }
        public int gameType { get; set; }
        #endregion
    }
}
