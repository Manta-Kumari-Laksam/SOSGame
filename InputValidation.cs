namespace SOSGame
{
    public class InputValidation
    {
        #region Validation Methods
        public string InputNotNullOrEmpty()
        {
            string input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a valid string.");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }

       public int GetChoice(int min, int max)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < min || choice > max)
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
            }
            return choice;
        }
       public int GetBoardSize()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");

            }
            while (choice < 3)
            {
                Console.WriteLine("Minimum size of the board must be 3 * 3. Please enter a valid choice.");
                choice = GetBoardSize();
            }
            return choice;
        }
       #endregion
    }
}
