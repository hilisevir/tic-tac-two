using GameBrain;

namespace ConsoleUI;


public static class Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance, SlidingGrid gridInstance)
    {
        Console.WriteLine();
        for (var y = 0; y < gameInstance.DimY; y++)
        {
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                
                if (x >= gridInstance.StartRow && x <= gridInstance.EndRow - 1
                                               && y >= gridInstance.StartCol
                                               && y <= gridInstance.EndCol)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x][y]) + " ");
                if (x != gameInstance.DimX - 1)
                {
                    Console.Write("|");
                }
            }
            Console.WriteLine();
            if (y == gameInstance.DimY - 1) continue;
            {
                for (var x = 0; x < gameInstance.DimX; x++)
                {
                    if (x >= gridInstance.StartRow && x <= gridInstance.EndRow
                                                   && y >= gridInstance.StartCol 
                                                   && y <= gridInstance.EndCol - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("---");
                    if (x == gameInstance.DimX - 1) continue;
                    if (x == gridInstance.EndRow)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("+");
                }

                Console.WriteLine();
            }
            
        }
        Console.WriteLine();
        Console.ResetColor();
    }

    private static string DrawGamePiece(EGamePiece piece) =>
        piece switch
        {
            EGamePiece.O => "O",
            EGamePiece.X => "X",
            _ => " "
        };
}