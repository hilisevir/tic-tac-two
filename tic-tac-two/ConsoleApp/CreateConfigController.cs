using DAL;
using GameBrain;

namespace ConsoleApp;

public static class CreateConfigController
{
    public static string MainLoopCreateConfig()
    {
        string? input;

        Console.Write("Are you sure you want to create new configuration? (y/n) ");
        if (Console.ReadLine()?.ToLower() == "n")
        {
            return "Exit";
        }
        do
        {
            Console.WriteLine("Create a new game configuration.");

            // Get the game name
            Console.Write("Enter game name: ");
            string? name;
            do
            {
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.Write("Game name cannot be empty. Please enter a valid name: ");
                }
            } while (string.IsNullOrWhiteSpace(name));

            // Get board size (width and height)
            var boardWidth = GetValidatedInteger("Enter board size width: ", minValue: 5);
            var boardHeight = GetValidatedInteger("Enter board size height: ", minValue: 5);

            // Get grid size (width and height)
            var gridWidth = GetValidatedInteger("Enter grid size width: ", minValue: 3, maxValue: boardWidth - 1);
            var gridHeight = GetValidatedInteger("Enter grid size height: ", minValue: 3, maxValue: boardHeight - 1);

            // Get win condition (number of pieces needed to win)
            int winCondition;
            do
            {
                winCondition = GetValidatedInteger($"Enter win condition (1-{Math.Min(gridWidth, gridHeight)}): ", minValue: 1, maxValue: Math.Min(gridWidth, gridHeight));
                if (winCondition > gridWidth || winCondition > gridHeight)
                {
                    Console.WriteLine("Win condition cannot exceed grid dimensions. Please try again.");
                }
            } while (winCondition > gridWidth || winCondition > gridHeight);

            // Get the number of pieces for each player
            var player1PieceCount = GetValidatedInteger("Enter number of pieces for Player 1: ", minValue: winCondition);
            var player2PieceCount = GetValidatedInteger("Enter number of pieces for Player 2: ", minValue: winCondition);
            
            var movePieceAfterNMoves = GetValidatedInteger("Enter number of pieces that need to be placed at the beginning of the game: ",
                minValue: 0, maxValue: Math.Min(player1PieceCount, player2PieceCount));
            
            
            
            var newConfig = new GameConfiguration
            {
                Name = name,
                BoardWidth = boardWidth,
                BoardHeight = boardHeight,
                GridWidth = gridWidth,
                GridHeight = gridHeight,
                WinCondition = winCondition,
                MovePieceAfterNMoves = movePieceAfterNMoves,
                Player1PieceAmount = player1PieceCount,
                Player2PieceAmount = player2PieceCount
            };

            
            RepositoryHelper.ConfigRepository.SaveConfiguration(newConfig);
            Console.WriteLine($"Game configuration '{name}' has been added.");
            
            
            Console.WriteLine("Do you want to create another configuration? (y/n): ");
            input = Console.ReadLine();

        } while (input?.ToLower() == "y");

        return "Configuration process completed.";
    }
    
    private static int GetValidatedInteger(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
    {
        int value;
        bool isValid;
        do
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            isValid = int.TryParse(input, out value) && value >= minValue && value <= maxValue;

            if (!isValid)
            {
                Console.WriteLine($"Invalid input. Please enter a valid integer between {minValue} and {maxValue}.");
            }
        } while (!isValid);

        return value;
    }
}
