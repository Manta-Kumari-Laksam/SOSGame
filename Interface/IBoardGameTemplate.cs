namespace SOSGame.Interface
{
    public interface IBoardGameTemplate
    {
        #region Public Method declarations
        public bool IsGameOver();
        public void ShowWinner();
        public void UndoMove();
        public void RedoMove();
        public void StartGame();
        #endregion
    }
}
