namespace GameBrain;

public class TicTacTwoBrain
{
    public readonly GameState GameState;
    
    // Constructor for saved game
    public TicTacTwoBrain(GameState gameState, SlidingGrid slidingGrid)
    {
        GameState = new GameState(gameState.GameBoard, gameState.GameConfiguration, slidingGrid, gameState.MadeMoves,
            gameState.GamePassword, gameState.NextMoveBy, gameState.Id, gameState.Player1PieceAmount,
            gameState.Player2PieceAmount, gameState.Name);
    }
    
    // Constructor for new game
    public TicTacTwoBrain(GameConfiguration gameConfiguration, SlidingGrid slidingGrid)
    {
        var gameBoard = new EGamePiece[gameConfiguration.BoardWidth][];
        for (var x = 0; x < gameBoard.Length; x++)
        {
            gameBoard[x] = new EGamePiece[gameConfiguration.BoardHeight];
        }
        GameState = new GameState(
            gameBoard,
            gameConfiguration,
            slidingGrid);
    }

    public EGamePiece[][] GameBoard => GetBoard();

    public int DimX => GameState.GameBoard.Length;
    public int DimY => GameState.GameBoard[0].Length;

    private int Player1Pieces => GameState.Player1PieceAmount;
    private int Player2Pieces => GameState.Player2PieceAmount;

    private EGamePiece[][] GetBoard()
    {
        var copyOffBoard = new EGamePiece[GameState.GameBoard.GetLength(0)][];
        for (var x = 0; x < GameState.GameBoard.Length; x++)
        {
            copyOffBoard[x] = new EGamePiece[GameState.GameBoard[x].Length];
            for (var y = 0; y < GameState.GameBoard[x].Length; y++)
            {
                copyOffBoard[x][y] = GameState.GameBoard[x][y];
            }
        }
        return copyOffBoard;
    }

    public GameState GetGameState(string gameName)
    {
        GameState.Name = gameName;
        return GameState;
    }

    public EGamePiece? CheckWinCondition(EGamePiece pieceToCheck)
    {
        for (var r = 0; r < 2; r++)
        {
            int count;
            // Check horizontal
            for (var y = GameState.SlidingGrid.StartCol; y < GameState.SlidingGrid.EndCol + 1; y++)
            {
                count = 0;
                for (var x = GameState.SlidingGrid.StartRow; x < GameState.SlidingGrid.EndRow + 1; x++)
                {
                    if (GameState.GameBoard[x][y] != pieceToCheck) continue;
                    count++;
                    if (count < GameState.GameConfiguration.WinCondition) continue;
                    Console.WriteLine("Win Found!");
                    return pieceToCheck;
                }
            }

            // Check vertical
            for (var x = GameState.SlidingGrid.StartRow; x < GameState.SlidingGrid.EndRow + 1; x++)
            {
                count = 0;
                for (var y = GameState.SlidingGrid.StartCol; y < GameState.SlidingGrid.EndCol + 1; y++)
                {
                    if (GameState.GameBoard[x][y] != pieceToCheck) continue;
                    count++;
                    if (count < GameState.GameConfiguration.WinCondition) continue;
                    Console.WriteLine("Win Found!");
                    return pieceToCheck;
                }
            }
            count = 0;

            
            for (var i = 0; i < GameState.GameConfiguration.WinCondition; i++)
            {
                if (GameState.GameBoard[GameState.SlidingGrid.StartRow + i][GameState.SlidingGrid.StartCol + i] != pieceToCheck) continue;
                count++;
                if (count < GameState.GameConfiguration.WinCondition) continue;
                Console.WriteLine("Win Found!");
                return pieceToCheck;
            }
            count = 0;
            
            for (var i = 0; i < GameState.GameConfiguration.WinCondition; i++)
            {
                if (GameState.GameBoard[GameState.SlidingGrid.EndRow - i][GameState.SlidingGrid.StartCol + i] != pieceToCheck) continue;
                count++;
                Console.WriteLine(count);
                if (count < GameState.GameConfiguration.WinCondition) continue;
                Console.WriteLine("Win Found!");
                return pieceToCheck;
            }
        }

        return null; // No win found
    }
    // Allow users place one of the pieces that still in their hand
    public void MakeAMove(int x, int y)
    {
        switch (GameState.NextMoveBy)
        {
            case EGamePiece.X when Player1Pieces == 0:
                throw new ApplicationException("Player X has no pieces left to place.");
            case EGamePiece.O when Player2Pieces == 0:
                throw new ApplicationException("Player O has no pieces left to place.");
        }
        
        try
        {
            if (GameState.GameBoard[x][y] != EGamePiece.Empty)
            {
                throw new ApplicationException("This place is already occupied. Choose another one.");
            }
        }
        catch (IndexOutOfRangeException)
        {
            throw new IndexOutOfRangeException("Piece were placed out of border bounds. Please try again.");
        }
        GameState.GameBoard[x][y] = GameState.NextMoveBy;
        SubtractPieces();
        
        // flip the next piece
        GameState.NextMoveBy = GameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        GameState.MadeMoves--;
        
    }
    
    // Allows user in the beginning of the game place the grid on any location of the board
    public bool PlaceAGrid(SlidingGrid gridInstance, int x=-1, int y=-1)
    { 
        GameState.SlidingGrid.GridCenterX = x;
        GameState.SlidingGrid.GridCenterY = y;

        gridInstance.GetGridBounds();

        return GameState.SlidingGrid.StartRow >= 0 &&
               GameState.SlidingGrid.EndRow <= GameState.GameConfiguration.BoardWidth - 1 &&
               GameState.SlidingGrid.StartCol >= 0 && GameState.SlidingGrid.EndRow <=
               GameState.GameConfiguration.BoardHeight - 1;
    }
    private void SubtractPieces()
    {
        if (GameState.NextMoveBy == EGamePiece.X)
        {
            GameState.Player1PieceAmount--;
        }
        else
        {
            GameState.Player2PieceAmount--;
        }
    }

    // Allows user to move the grid one spot in any direction
    public void MoveAGrid(string userInput)
    {
        if (!CheckMoveCommand(userInput))
        {
            throw new ApplicationException($"Invalid command: '{userInput}'. Please try again.");
        }
        try
        {
            switch (userInput.ToUpper())
            {
                case ("R"):
                    GameState.SlidingGrid.MoveRight();
                    break;
                case ("L"):
                    GameState.SlidingGrid.MoveLeft();
                    break;
                case ("U"):
                    GameState.SlidingGrid.MoveUp();
                    break;
                case ("D"):
                    GameState.SlidingGrid.MoveDown();
                    break;
                case ("UR"):
                    GameState.SlidingGrid.MoveUpRight();
                    break;
                case ("UL"):
                    GameState.SlidingGrid.MoveUpLeft();
                    break;
                case ("DR"):
                    GameState.SlidingGrid.MoveDownRight();
                    break;
                case ("DL"):
                    GameState.SlidingGrid.MoveDownLeft();
                    break;
            }
        }
        catch (IndexOutOfRangeException e)
        {
            throw new ApplicationException(e.Message);
        }
        
        GameState.NextMoveBy = GameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }
    
    
    // Allows user to change position of already placed piece
    public void ChangePiecePosition(int newX, int newY, int oldX, int oldY)
    {
        EGamePiece pieceToMove;

        if (GameState.GameBoard[oldX][oldY] == GameState.NextMoveBy)
        {
            pieceToMove = GameState.GameBoard[oldX][oldY];
        }else
        {
            throw new ApplicationException($"You can move only your pieces that are in the grid! Your chose was: {GameState.GameBoard[oldX][oldY]}");
        }
        
        if (GameState.GameBoard[newX][newY] != EGamePiece.Empty)
        {
            throw new ApplicationException($"You can place a piece only on empty cells. Your chose was: {GameState.GameBoard[newX][newY]}");
        }
        
        
        GameState.GameBoard[oldX][oldY] = EGamePiece.Empty;
        GameState.GameBoard[newX][newY] = pieceToMove;
        
        GameState.NextMoveBy = GameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
    }
    
    
    // Check if user provided the valid coordinates 
    public bool CheckCoordinates(string[] coordinates)
    {
        if (coordinates.Length == 2 && int.TryParse(coordinates[0], out var x)
                                    && int.TryParse(coordinates[1], out var y)) return true;
         throw new ApplicationException($"Invalid input: '{string.Join(',', coordinates)}'. Please try again. " +
                                        $"Coordinates should be in format x,y");
    }
    
    // Check if piece inside the grid
    public void CheckIfPieceInGrid(string[] coordinates)
    {
        if(!CheckCoordinates(coordinates)) return;
        
        var inputX = int.Parse(coordinates[0]);
        var inputY = int.Parse(coordinates[1]);

        if (!(GameState.SlidingGrid.StartRow <= inputX
              && GameState.SlidingGrid.EndRow >= inputX
              && GameState.SlidingGrid.StartCol <= inputY
              && GameState.SlidingGrid.EndCol >= inputY))
        {
            throw new ApplicationException($"Invalid input: '{string.Join(',', coordinates)}'. Please try again. " +
                                           $"You can't take or place a piece out of bounds of the grid.");
        }
        
    }
    
    // Check if move command is valid
    private static bool CheckMoveCommand(string? command)
    {
        if (command == null) return false;
        
        string[] commands = ["R", "L", "U", "D", "UR", "UL", "DR", "DL"];
        
        return commands.Contains(command.ToUpper());

    }
}