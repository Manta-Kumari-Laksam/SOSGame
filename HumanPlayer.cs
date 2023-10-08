using SOSGame.Interface;

namespace SOSGame
{
    public class HumanPlayer : AbstractPlayer
    {
        #region Constructor
        public HumanPlayer(string playerName, int playerScore) : base(playerName, playerScore)
        {
        }
        #endregion

        #region Overridden Methods
        public override string[] GenerateMove(Board board, int boardSize)
        {
            Console.Write("Enter row col symbol: (e.g. '0 1 S') ");
            string input = Console.ReadLine();
            string[] inputValues = input.Split(' ');
            return inputValues;
        }

        public override void HasScored()
        {
            this.score += 2;
        }
        #endregion
    }
}
