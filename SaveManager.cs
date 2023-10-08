using SOSGame.Interface;

namespace SOSGame
{
    public class SaveManager
    {
        #region Private properties
        private GameState gameState = new GameState();
        private string path;
        #endregion

        #region Constructors
        public SaveManager()
        {
            this.path = string.Empty;
        }
        public SaveManager(string path, Board board, AbstractPlayer player1, AbstractPlayer player2, int boardSize, int currentPlayer, int gameType)
        {
            gameState.board = board;
            gameState.player1 = player1;
            gameState.player2 = player2;
            gameState.boardSize = boardSize;
            gameState.currentPlayer = currentPlayer;
            gameState.gameType = gameType;
            this.path = path;
        }
        #endregion

        #region Save Game
        public void SaveGame()
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("S," + gameState.boardSize);
                sw.WriteLine("C," + gameState.currentPlayer);
                sw.WriteLine("GT," + gameState.gameType);
                for (int i = 0; i < gameState.boardSize; i++)
                {
                    for (int j = 0; j < gameState.boardSize; j++)
                    {
                        if (gameState.board[i, j] != ' ')
                        {
                            sw.WriteLine("B," + i + "," + j + "," + gameState.board[i, j]);
                        }
                    }
                }
                sw.WriteLine("P1," + gameState.player1.ShowName() + "," + gameState.player1.ShowScore());
                sw.WriteLine("P2," + gameState.player2.ShowName() + "," + gameState.player2.ShowScore());                
            }
        }
        #endregion

        #region Load Game
        public GameState LoadGame()
        {
            try
            {
                path = (path == string.Empty) ? @"D:\temp\SaveSOSGame.txt" : path;
                GameState gameState = new GameState();
                using (StreamReader sr = File.OpenText(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] rowData = line.Split(',');
                        switch (rowData[0])
                        {
                            case "S":
                                int size = int.Parse(rowData[1]);
                                gameState.boardSize = size;
                                gameState.board = new Board(size);
                                break;
                            case "C":
                                gameState.currentPlayer = int.Parse(rowData[1]);
                                break;
                            case "GT":
                                gameState.gameType = int.Parse(rowData[1]);
                                break;
                            case "B":
                                int row = int.Parse(rowData[1]);
                                int col = int.Parse(rowData[2]);
                                gameState.board[row, col] = char.Parse(rowData[3]);
                                break;
                            case "P1":
                                gameState.player1 = new HumanPlayer(rowData[1], int.Parse(rowData[2]));
                                break;
                            case "P2":
                                if (gameState.gameType == 1)
                                {
                                    gameState.player2 = new HumanPlayer(rowData[1], int.Parse(rowData[2]));
                                }
                                else
                                {
                                    gameState.player2 = new ComputerPlayer(rowData[1], int.Parse(rowData[2]));
                                }
                                break;
                        }
                    }
                }
                return gameState;
            }
            catch (FileNotFoundException ex)
            {
                // Handle the exception
                Console.WriteLine("File not found: " + ex.FileName);
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine("An error occurred: " + ex.Message);
                Environment.Exit(0);
            }
            return gameState;
        }
        #endregion
    }
}
