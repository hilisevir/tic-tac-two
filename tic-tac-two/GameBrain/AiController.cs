namespace GameBrain;

public class AiController(TicTacTwoBrain gameInstance)
{
    public void MakeDecision()
    {
        if (gameInstance.GameState.NextMoveBy == EGamePiece.X && gameInstance.GameState.Player1PieceAmount == 0 ||
            gameInstance.GameState.NextMoveBy == EGamePiece.O && gameInstance.GameState.Player2PieceAmount == 0)
        {
            MakeStrategicMove();
        }
        else
        {
            PlaceBestPiece();
        }
    }

    private void PlaceBestPiece()
    {
        var bestMove = FindWinningMove(gameInstance.GameState.NextMoveBy) ??
                       FindBlockingMove() ??
                       FindStrategicPosition();

        if (bestMove != null)
        {
            gameInstance.MakeAMove(bestMove.Value.x, bestMove.Value.y);
        }
    }

    private (int x, int y)? FindWinningMove(EGamePiece piece)
    {
        for (var i = 0; i < gameInstance.GameBoard.Length; i++)
        {
            for (var j = 0; j < gameInstance.GameBoard[i].Length; j++)
            {
                if (gameInstance.GameBoard[i][j] != EGamePiece.Empty) continue;
                
                gameInstance.GameBoard[i][j] = piece;
                if (CheckWinCondition(piece))
                {
                    gameInstance.GameBoard[i][j] = EGamePiece.Empty;
                    return (i, j);
                }
                gameInstance.GameBoard[i][j] = EGamePiece.Empty;
            }
        }
        return null;
    }

    private (int x, int y)? FindBlockingMove()
    {
        var opponentPiece = gameInstance.GameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return FindWinningMove(opponentPiece);
    }

    private (int x, int y)? FindStrategicPosition()
    {
        var center = gameInstance.GameBoard.Length / 2;
        if (gameInstance.GameBoard[center][center] == EGamePiece.Empty)
        {
            return (center, center);
        }

        // Если центр занят, выбрать первую свободную клетку.
        for (int i = 0; i < gameInstance.GameBoard.Length; i++)
        {
            for (int j = 0; j < gameInstance.GameBoard[i].Length; j++)
            {
                if (gameInstance.GameBoard[i][j] == EGamePiece.Empty)
                {
                    return (i, j);
                }
            }
        }
        return null;
    }

    private bool CheckWinCondition(EGamePiece piece)
    {
        var n = gameInstance.GameBoard.Length;
        
        for (var i = 0; i < n; i++)
        {
            if (CheckLine(i, 0, 0, 1, piece) || CheckLine(0, i, 1, 0, piece))
            {
                return true;
            }
        }
        
        return CheckLine(0, 0, 1, 1, piece) || CheckLine(0, n - 1, 1, -1, piece);
    }

    private bool CheckLine(int startX, int startY, int stepX, int stepY, EGamePiece piece)
    {
        var count = 0;
        for (var i = 0; i < gameInstance.GameBoard.Length; i++)
        {
            var x = startX + i * stepX;
            var y = startY + i * stepY;
            if (gameInstance.GameBoard[x][y] == piece)
            {
                count++;
            }
            else
            {
                break;
            }
        }
        return count == gameInstance.GameBoard.Length;
    }

    private void MakeStrategicMove()
    {
        
        try
        {
            gameInstance.MoveAGrid("U");
        }
        catch (ApplicationException)
        {
            
        }
    }
}
