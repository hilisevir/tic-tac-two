using DAL;
using GameBrain;

namespace ConsoleApp;

public static class OptionsController
{
    // private static readonly IConfigRepository ConfigRepository = new ConfigRepositoryJson();

    private static AppDbContextFactory factory = new AppDbContextFactory();
    private static AppDbContext dbContext = factory.CreateDbContext(Array.Empty<string>());
    private static IConfigRepository ConfigRepository = new ConfigRepositoryDb(dbContext);
    
    public static string MainLoop()
        {
            string? input;
            do
            {
                Console.WriteLine("Create a new game configuration.");

                // Get the game name
                Console.Write("Enter game name: ");
                string name = Console.ReadLine();

                // Get board size (width and height)
                Console.Write("Enter board size width: ");
                int boardWidth = int.Parse(Console.ReadLine());
                
                Console.Write("Enter board size height: ");
                int boardHeight = int.Parse(Console.ReadLine());

                // Get grid size (width and height)
                Console.Write("Enter grid size width: ");
                int gridWidth = int.Parse(Console.ReadLine());
                
                Console.Write("Enter grid size height: ");
                int gridHeight = int.Parse(Console.ReadLine());

                // Get win condition (number of pieces needed to win)
                Console.Write("Enter win condition (e.g., 3 for 3-in-a-row): ");
                int winCondition = int.Parse(Console.ReadLine());

                // Get the number of pieces for each player
                Console.Write("Enter number of pieces for Player 1: ");
                int player1PieceCount = int.Parse(Console.ReadLine());
                
                Console.Write("Enter number of pieces for Player 2: ");
                int player2PieceCount = int.Parse(Console.ReadLine());

                // Fill Player1 and Player2 pieces
                var player1Pieces = new List<EGamePiece>();
                for (int i = 0; i < player1PieceCount; i++)
                {
                    player1Pieces.Add(EGamePiece.X);
                }

                var player2Pieces = new List<EGamePiece>();
                for (int i = 0; i < player2PieceCount; i++)
                {
                    player2Pieces.Add(EGamePiece.O);
                }

                // Create the custom game configuration
                var newConfig = new GameConfiguration
                {
                    Name = name,
                    BoardSizeWidth = boardWidth,
                    BoardSizeHeight = boardHeight,
                    GridSizeWidth = gridWidth,
                    GridSizeHeight = gridHeight,
                    WinCondition = winCondition,
                    Player1Pieces = player1Pieces,
                    Player2Pieces = player2Pieces
                };

                // Add the new configuration to the repository
                ConfigRepository.SaveConfiguration(newConfig);
                Console.WriteLine($"Game configuration '{name}' has been added.");
                
                // Ask user if they want to create another configuration
                Console.WriteLine("Do you want to create another configuration? (y/n): ");
                input = Console.ReadLine();

            } while (input?.ToLower() == "y");

            return "Configuration process completed."; 
        }
}