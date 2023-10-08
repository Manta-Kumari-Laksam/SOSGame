using SOSGame.Interface;

namespace SOSGame
{
    public class Board : AbstractBoardDesign
    {
        #region Private Properties
        private List<string> Coordinates = new List<string>();
        #endregion

        #region Constructor
        public Board(int boardSize) : base(boardSize)
        {   
            
        }
        #endregion

        #region Indexer
        public char this[int row, int col]
        {
            get { return board[row, col]; }
            set { board[row, col] = value; }
        }
        #endregion

        #region  Overridden Method
        public override bool CheckForWin()
        {
            bool horizontal = false;
            bool vertical = false;
            bool diagonal = false;
            bool antiDiagonal = false;
            bool horizontalFlag = false;
            bool verticalFlag = false;
            bool diagonalFlag = false;
            bool antiDiagonalFlag = false;
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (i <= boardSize - 3)
                    {
                        horizontal = CheckSequence(i, j, 1, 0);  // Check horizontal
                        horizontalFlag = horizontal ? horizontal : horizontalFlag;
                        if (j <= boardSize - 3)
                        {
                            diagonal = CheckSequence(i, j, 1, 1); // Check diagonal
                            diagonalFlag = diagonal ? diagonal : diagonalFlag;
                        }
                    }

                    if (j <= boardSize - 3)
                    {
                        vertical = CheckSequence(i, j, 0, 1); // Check vertical
                        verticalFlag = vertical ? vertical : verticalFlag;
                        if (i >= 2)
                        {
                            antiDiagonal = CheckSequence(i, j, -1, 1); // Check anti-diagonal
                            antiDiagonalFlag = antiDiagonal ? antiDiagonal : antiDiagonalFlag;
                        }
                    }
                }
            }
            return (horizontalFlag || verticalFlag || diagonalFlag || antiDiagonalFlag);
        }
        public bool CheckSequence(int startRow, int startCol, int rowStep, int colStep)
        {
            bool flag = true;
            string sequence = "SOS";
            string sosCoordinates = "";
            for (int i = 0; i < 3; i++)
            {
                int row = startRow + i * rowStep;
                int col = startCol + i * colStep;

                if (board[row, col] != sequence[i])
                {
                    flag = false; // Exit early if the sequence doesn't match
                    break;
                }
                sosCoordinates += row.ToString() + col.ToString();

            }

            if (Coordinates.Contains(sosCoordinates))
            {
                return false;
            }
            else
            {
                Coordinates.Add(sosCoordinates);
                return flag;
            }

        }
        #endregion
    }
}
