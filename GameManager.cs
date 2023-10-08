using SOSGame.Interface;

namespace SOSGame
{
    public class GameManager : IBoardGameTemplate
    {
        #region Properties
        private int boardSize;
        private Board board;
        private AbstractPlayer player1;
        private AbstractPlayer player2;      
        private Stack<UndoRedoManager> undoStack = new Stack<UndoRedoManager>();
        private Stack<UndoRedoManager> redoStack = new Stack<UndoRedoManager>();
        public int gameType = 1;
        public int currentPlayer = 1;
        #endregion

        #region Constructors
        public GameManager(int size)
        {
            boardSize = size;
            board = new Board(boardSize);
        }
        public GameManager(Board board, AbstractPlayer player1, AbstractPlayer player2, int currentPlayer, int boardSize, int gameType)
        {
            this.board = new Board(boardSize);
            this.board = board;
            this.player1 = player1;
            this.player2 = player2;
            this.currentPlayer = currentPlayer;
            this.boardSize = boardSize;
            this.gameType = gameType;
        }
        #endregion

        #region Public Methods
        public bool IsGameOver()
        {
            return board.IsBoardFull();
        }
        public void ShowWinner()
        {
            if (player1.ShowScore() > player2.ShowScore())
            {
                Console.WriteLine($"Player 1 [{player1.ShowName()}] won!");
            }
            else if (player1.ShowScore() < player2.ShowScore())
            {
                Console.WriteLine($"Player 2 [{player2.ShowName()}] won!");
            }
            else if(player1.ShowScore() == 0 && player2.ShowScore() == 0)
            {
                //show nothing
            }
            else
            {
                Console.WriteLine("It's a draw!");
            }
        }
        public void UndoMove()
        {
            if (undoStack.Count > 0)
            {
                UndoRedoManager lastMove = undoStack.Pop();
                int row = lastMove.Row;
                int column = lastMove.Column;
                int symbol = lastMove.Symbol;
                board[row, column] = ' '; // Clear the cell

                redoStack.Push(lastMove);

                // Switch players
                currentPlayer = lastMove.Player;
            }
            else
            {
                Console.WriteLine("No more moves to undo!");
            }
        }
        public void RedoMove()
        {
            if (redoStack.Count > 0)
            {
                UndoRedoManager nextMove = redoStack.Pop();
                int row = nextMove.Row;
                int column = nextMove.Column;
                board[row, column] = nextMove.Symbol; // Apply the move again

                undoStack.Push(nextMove);

                // Switch players
                currentPlayer = nextMove.Player;
            }
            else
            {
                Console.WriteLine("No more moves to redo!");
            }
        }              
        public void StartGame()
        {
            Console.WriteLine("Enter 'u' to undo, 'r' to redo, or 'q' to quit: ");
            InputValidation inputValidation = new InputValidation();

            while (!board.IsBoardFull())
            {
                string currentPlayerName = currentPlayer == 1 ? player1.ShowName() : player2.ShowName();
                board.DrawBoard();
                Console.WriteLine($"Player {currentPlayerName}'s turn.");
                string[] inputValues;
                if (gameType == 2 && currentPlayer == 2)
                {
                    Console.WriteLine("Computer is thinking...");
                    System.Threading.Thread.Sleep(1000);
                    inputValues = player2.GenerateMove(board, boardSize);                    
                }
                else
                {
                    inputValues =  player1.GenerateMove(board, boardSize);

                }
               
                if (inputValues.Length == 3 && int.TryParse(inputValues[0], out int row) &&
                    int.TryParse(inputValues[1], out int col) && (inputValues[2] == "S" || inputValues[2] == "O"))
                {
                    char symbol = inputValues[2][0];
                    if (board.IsValidMove(row, col))
                    {
                        board.MakeMove(row, col, symbol);

                        UndoRedoManager undoRedoManager = new UndoRedoManager { Player = currentPlayer, Row = row, Column = col, Symbol = symbol };
                        undoStack.Push(undoRedoManager);
                        redoStack.Clear();

                        board.DrawBoard();
                        if (board.CheckForWin())
                        {
                            Console.Clear();                           
                            Console.WriteLine($"Nice move {currentPlayerName}. You scored!");
                            switch (currentPlayer)
                            {
                                case 1:                                    
                                    player1.HasScored();
                                    break;
                                case 2:
                                    player2.HasScored();
                                    break;
                            }
                        }
                        else if (board.IsBoardFull())
                        {
                            Console.Clear();
                            ShowWinner();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            currentPlayer = 3 - currentPlayer; // Switch players
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid move. Try again.");                   
                    }
                }
                else if (inputValues[0] == "u")
                {
                    Console.Clear();
                    UndoMove();
                }
                else if (inputValues[0] == "r")
                {
                    Console.Clear();
                    RedoMove();
                }
                else if (inputValues[0] == "q")
                {
                    Console.Clear();
                    Console.WriteLine("Save game? Enter 'y' to save or any other letter to simply quit.");
                    string choice = inputValidation.InputNotNullOrEmpty();
                    if(choice == "y")
                    {
                        string path = @"D:\temp\SaveSOSGame.txt"; //”D:\temp\SaveSOSGame.txt”
                        SaveManager saveManager = new SaveManager(path, board, player1, player2, boardSize,currentPlayer,gameType);
                        saveManager.SaveGame();
                    }                 
                    break;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid input. Use the format 'row col symbol'. Try Again.");
                }
            }
            
            if(board.IsBoardFull())
            {
                ShowWinner() ;  
            }
        }
        #endregion
    }

}
