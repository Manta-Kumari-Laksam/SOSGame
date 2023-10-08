using SOSGame;
using SOSGame.Interface;

public class SOSGameLauncher
{
    private static void Main(string[] args)
    {
        InputValidation inputValidation = new InputValidation();
        Console.WriteLine("Welcome to SOS Game!");

        Console.WriteLine("Do you want to reload or start a new game?");
        Console.WriteLine("1. New Game");
        Console.WriteLine("2. Reload");
        int choiceA = inputValidation.GetChoice(1, 2);

        if(choiceA == 1)
        {
            Console.Clear();
            Console.WriteLine("Do you want to play against the computer or a friend?");
            Console.WriteLine("1. Friend");
            Console.WriteLine("2. Computer");
            int choiceB = inputValidation.GetChoice(1, 2);

            Console.Clear();
            Console.Write("Enter board size (e.g., 3 for a 3x3 board): ");
            int boardSize = inputValidation.GetBoardSize();
            Board board = new Board(boardSize);

            GameManager sosGame = new GameManager(boardSize);
            
            Console.Clear();

            PlayerFactory player = new PlayerFactory();
           
            switch (choiceB)
            {
                case 1:
                    int gameType = 1; //For Human
                    Console.Write("Enter player 1 name: ");
                    string player1Name = inputValidation.InputNotNullOrEmpty();
                    AbstractPlayer player1 = player.GetPlayerType("Human", player1Name, 0);

                    Console.Write("Enter player 2 name: ");
                    string player2Name = inputValidation.InputNotNullOrEmpty();
                    AbstractPlayer player2 = player.GetPlayerType("Human",player2Name, 0);
                    sosGame = new GameManager(board,player1,player2,1,boardSize, gameType);
                    break;
                case 2:

                    Console.Write("Enter player 1 name: ");
                    player1Name = inputValidation.InputNotNullOrEmpty();
                    player1 = player.GetPlayerType("Human", player1Name, 0);
                    player2 = player.GetPlayerType("Computer", "Computer", 0);
                    gameType = 2; // For Computer
                    sosGame = new GameManager(board, player1, player2, 1, boardSize, gameType);
                    break;

            }
            Console.Clear(); 
            sosGame.StartGame();
        }
        else
        {
            Console.Clear();
            SaveManager saveManager = new SaveManager();
            GameState gameState = saveManager.LoadGame();
            GameManager reloadGame = new GameManager(gameState.board, gameState.player1, gameState.player2, gameState.currentPlayer, gameState.boardSize, gameState.gameType);
            reloadGame.StartGame();
        }

        Console.WriteLine("Thankyou for playing!");
    }
}