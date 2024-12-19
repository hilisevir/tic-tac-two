using DAL;
using Domain;
using GameBrain;
namespace ConsoleApp;



public static class GameController
{
    public static void GameLoop(SlidingGrid gridConstruct, TicTacTwoBrain gameInstance)
    {
        var flag = true;
        do
        {
            string? input;
            var pieceToCheck = gameInstance.GameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            var pieceWon = gameInstance.CheckWinCondition(pieceToCheck);
            if (pieceWon != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Congratulations {pieceWon}! You won!");
                Console.ResetColor();
                break;
            }
            
            if (gameInstance.GameState.MadeMoves > 0)
            {
                ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
                Console.WriteLine("");
                Console.WriteLine($"Make {gameInstance.GameState.MadeMoves} more moves to get other game options");
                input = "1";
                
            }
            else
            {
                ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
                Console.WriteLine(gameInstance.GameState.GamePassword);
                Console.WriteLine($"Player1: {new string('X', gameInstance.GameState.Player1PieceAmount)}");
                Console.WriteLine($"Player2: {new string('O', gameInstance.GameState.Player2PieceAmount)}");
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Place Piece - Place a new piece on the board.");
                Console.WriteLine("2. Move Piece - Move an existing piece to a different position inside the grid.");
                Console.WriteLine("3. Move Grid - Shift the entire grid to a new position.");
                Console.WriteLine("4. Save Game - Save current game.");
                Console.WriteLine("5. Exit");
                Console.Write("Enter the number of your choice: ");
                input = Console.ReadLine();
            }

            switch (input)
            {
                case "1":
                    Console.Write("Enter the coordinates <x,y> to place a piece: ");
                    input = Console.ReadLine();
                    var coordinateSplit = input.Split(',');
                    if (!gameInstance.CheckCoordinates(coordinateSplit)) break;
                    
                    var x = int.Parse(coordinateSplit[0]);
                    var y = int.Parse(coordinateSplit[1]);
                    try
                    {
                        gameInstance.MakeAMove(x, y);

                    }
                    catch (Exception e)
                    {
                        ThrowError(e.Message);
                    }
                    break;
                case "2":
                    Console.WriteLine("You can move one of your pieces that are in the grid to another place in the grid!");
                    Console.Write("Enter your piece position that you would like to move <x,y>: ");
                    input = Console.ReadLine();
                    try
                    {
                        var oldCor = input?.Split(',');
                        if (oldCor != null)
                        {
                            gameInstance.CheckIfPieceInGrid(oldCor);
                            var oldX = int.Parse(oldCor[0]);
                            var oldY = int.Parse(oldCor[1]);

                            Console.Write("Enter new position <x,y> for the piece you want to move: ");
                            var newInput = Console.ReadLine();
                            var newCor = newInput?.Split(',');
                            if (newCor != null)
                            {
                                gameInstance.CheckIfPieceInGrid(newCor);
                                var newX = int.Parse(newCor[0]);
                                var newY = int.Parse(newCor[1]);

                                gameInstance.ChangePiecePosition(newX, newY, oldX, oldY);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ThrowError(e.Message);
                    }
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
                    try
                    {
                        if (input != null) gameInstance.MoveAGrid(input);
                    }
                    catch (Exception e)
                    {
                        ThrowError(e.Message);
                    }
                    break;
                case "4":
                    Console.Write("Are you sure you want to save the current game? y/n: ");
                    input = Console.ReadLine() ?? "n";
                    if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.Write("Please, enter the name for the game or hit enter to use generic name: ");
                        input = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(input)) input = FileHelper.GetUniqueGameName();
                        
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
    // Method for standard error
    private static void ThrowError(string message)
    {
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.WriteLine();
        Console.ResetColor();
    }
}