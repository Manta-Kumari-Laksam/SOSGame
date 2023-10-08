namespace SOSGame.Interface
{
    public abstract class AbstractBoardDesign
    {
        #region Public Properties
        public int boardSize;
        public char[,] board;
        #endregion

        #region Constructors
        public AbstractBoardDesign(int boardSize)
        {
            this.boardSize = boardSize;
            this.board = new char[boardSize, boardSize];
            InitializeBoard();
        }
        #endregion

        #region Common Operations/Methods
        public void InitializeBoard()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    board[row, col] = ' ';
                }
            }
        }
        public void DrawBoardOutline()
        {
            string line = "-";
            for (int i = 0; i < boardSize; i++)
            {
                line += "----";
            }
            Console.WriteLine(line);
        }
        public void DrawBoard()
        {
            DrawBoardOutline();
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    Console.Write("| " + board[row, col] + " ");
                }
                Console.WriteLine("|");
                DrawBoardOutline();
            }
        }
        public bool IsBoardFull()
        {
            for (int row = 0; row < boardSize; row++)
            {
                for (int col = 0; col < boardSize; col++)
                {
                    if (board[row, col] == ' ') return false;
                }
            }
            return true;
        }
        public bool IsValidMove(int row, int col)
        {
            return row >= 0 && row < boardSize && col >= 0 && col < boardSize && board[row, col] == ' ';
        }       
        public void MakeMove(int row, int col, char symbol)
        {
            board[row, col] = symbol;
        }
        #endregion

        #region Abstract Methods
        public abstract bool CheckForWin();
        #endregion
    }
}
