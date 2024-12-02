using DAL;
using GameBrain;
namespace ConsoleApp;



public static class GameController
{
    public static void GameLoop(SlidingGrid gridConstruct, TicTacTwoBrain gameInstance)
    {
        var flag = true;
        do
        {
            ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Place Piece - Place a new piece on the board.");
            Console.WriteLine("2. Move Piece - Move an existing piece to a different position inside the grid.");
            Console.WriteLine("3. Move Grid - Shift the entire grid to a new position.");
            Console.WriteLine("4. Save Game - Save current game.");
            Console.WriteLine("5. Exit");
            Console.Write("Enter the number of your choice: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter the coordinates <x,y> to place a piece: ");
                    input = Console.ReadLine();
                    gameInstance.MakeAMove(input);
                    break;
                case "2":
                    Console.WriteLine("You can move one of your pieces that are in the grid to another place in the grid!");
                    Console.Write("Enter your piece position that you would like to move <x,y>: ");
                    input = Console.ReadLine();
                    gameInstance.ChangePiecePosition(input);
                    break;
                case "3":
                    Console.WriteLine("Give me a command to move the grid:");
                    Console.WriteLine("Use the following commands:");
                    Console.WriteLine("U  - Move Up");
                    Console.WriteLine("D  - Move Down");
                    Console.WriteLine("L  - Move Left");
                    Console.WriteLine("R  - Move Right");
                    Console.WriteLine("UR - Move Up-Right");
                    Console.WriteLine("UL - Move Up-Left");
                    Console.WriteLine("DR - Move Down-Right");
                    Console.WriteLine("DL - Move Down-Left");
                    Console.Write("Enter your command: ");
                    input = Console.ReadLine();
                    gameInstance.MoveAGrid(input);
                    break;
                case "4":
                    Console.Write("Are you sure you want to save the current game? y/n: ");
                    input = Console.ReadLine() ?? "n";
                    if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.Write("Please, enter the name for the game or hit enter to use generic name: ");
                        input = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(input)) input = GetUniqueGameName();
                        
                        RepositoryHelper.GameRepository.SaveGame(gameInstance.GetGameState(input));
                    }
                    break;
                case "5":
                    Console.Write("Are you sure you want to exit? y/n: ");
                    input = Console.ReadLine() ?? "n";
                    if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        flag = false;
                    }
                    break;
            }       
            

        } while (flag);

    }

    
    private static string GetUniqueGameName()
    {
        var count = 0;
        var res = RepositoryHelper.GameRepository.GetGameNames();
        foreach (var t in res)
        {
            if (t.Contains("New Save "))
            {
                count++;
            }
        }
        return "New Save " + (count + 1);
    }
}