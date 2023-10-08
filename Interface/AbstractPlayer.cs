namespace SOSGame.Interface
{
    public abstract class AbstractPlayer
    {
        #region Public Properties
        public string name;
        public int score;
        #endregion

        #region Constructor
        public AbstractPlayer()
        {
            this.name = string.Empty;
            this.score = 0;
        }
        public AbstractPlayer(string playerName, int playerScore)
        {
            this.name = playerName;
            this.score = playerScore;
        }
        #endregion

        #region Common Methods
        public int ShowScore() { return score; }
        public string ShowName() { return name; }
        #endregion

        #region Abstract methods
        public abstract void HasScored();
        public abstract string[] GenerateMove(Board board, int boardSize);
        #endregion

    }
}
