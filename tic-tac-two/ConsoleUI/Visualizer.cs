using GameBrain;

namespace ConsoleUI;


public static class Visualizer
{
    public static void DrawBoard(TicTacTwoBrain gameInstance, SlidingGrid gridBounds)
    {

        var (startRow, endRow, startCol, endCol) = gridBounds.GetGridBounds();
        
        for (var y = 0; y < gameInstance.DimY; y++)
        {
            for (var x = 0; x < gameInstance.DimX; x++)
            {
                
                if (x >= startRow && x <= endRow - 1 && y >= startCol && y <= endCol)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                
                Console.Write(" " + DrawGamePiece(gameInstance.GameBoard[x, y]) + " ");
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
                    if (x >= startRow && x <= endRow && y >= startCol && y <= endCol - 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    } else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("---");
                    if (x == gameInstance.DimX - 1) continue;
                    if (x == endRow)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("+");
                }

                Console.WriteLine();
            }
            
        }
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