using SOSGame.Interface;

namespace SOSGame
{
    public class ComputerPlayer : AbstractPlayer
    {
        #region Constructor
        public ComputerPlayer(string playerName, int playerScore) : base(playerName, playerScore)
        {
        }
        #endregion

        #region Overridden Methods
        public override void HasScored()
        {
            this.score += 2;
        }

        public override string[] GenerateMove(Board board, int boardSize)
        {
            string[] inputValues;
            Random random = new Random();
            char[] letters = { 'S', 'O' };

            int randomIndex = random.Next(letters.Length); // Generate a random index (0 or 1)

            char randomLetter = letters[randomIndex];

            int row, col;
            do
            {
                row = random.Next(boardSize);
                col = random.Next(boardSize);
            } while (board[row, col] != ' ');

            string input = row.ToString() + ' ' + col.ToString() + ' ' + randomLetter;
            inputValues = input.Split(' ');
            return inputValues;

        }
        #endregion
    }
}
