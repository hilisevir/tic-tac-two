namespace GameBrain;

public class TicTacTwoBrain
{
    private static GameState _gameState;
    public TicTacTwoBrain(GameConfiguration gameConfiguration, SlidingGrid slidingGrid)
    {
        _gameState = new GameState(
            new EGamePiece[
                gameConfiguration.BoardSizeWidth,
                gameConfiguration.BoardSizeHeight],
            gameConfiguration, slidingGrid);
    }
    
    

    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameState.GameBoard = value;
    }
    
    public int DimX => _gameState.GameBoard.GetLength(0);
    public int DimY => _gameState.GameBoard.GetLength(1);
    public static List<EGamePiece> Player1Pieces => _gameState.GameConfiguration.Player1Pieces;
    public static List<EGamePiece> Player2Pieces => _gameState.GameConfiguration.Player2Pieces;
    
    private EGamePiece[,] GetBoard()
    {
        var copyOffBoard = new EGamePiece[_gameState.GameBoard.GetLength(0), _gameState.GameBoard.GetLength(1)];
        for (var x = 0; x < _gameState.GameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameState.GameBoard.GetLength(1); y++)
            {
                copyOffBoard[x, y] = _gameState.GameBoard[x, y];
            }
        }
        return copyOffBoard;
    }


   public static bool CheckWinCondition(EGamePiece gamePiece)
{
    int count = 0;
    // Check horizontal
    for (int y = _gameState.SlidingGrid.StartCol; y < _gameState.SlidingGrid.EndCol + 1; y++)
    {
        count = 0;
        for (int x = _gameState.SlidingGrid.StartRow; x < _gameState.SlidingGrid.EndRow + 1; x++)
        {
            if (_gameState.GameBoard[x, y] == gamePiece)
            {
                count++;
                if (count >= _gameState.GameConfiguration.WinCondition)
                {
                    return true; // Win found
                }
            }
            else
            {
                count = 0; // Reset count if the sequence breaks
            }
        }
    }

    // Check vertical
    for (int x = _gameState.SlidingGrid.StartRow; x < _gameState.SlidingGrid.EndRow + 1; x++)
    {
        count = 0;
        for (int y = _gameState.SlidingGrid.StartCol; y < _gameState.SlidingGrid.EndCol + 1; y++)
        {
            if (_gameState.GameBoard[x, y] == gamePiece)
            {
                count++;
                if (count >= _gameState.GameConfiguration.WinCondition)
                {
                    return true; // Win found
                }
            }
            else
            {
                count = 0; // Reset count if the sequence breaks
            }
        }
    }
    count = 0;

    
    for (int i = 0; i < _gameState.GameConfiguration.WinCondition; i++)
    {
        if (_gameState.GameBoard[_gameState.SlidingGrid.StartRow + i, _gameState.SlidingGrid.StartCol + i] != gamePiece) continue;
        count++;
        if (count >= _gameState.GameConfiguration.WinCondition)
        {
            return true; // Win found
        }
    }
    count = 0;
    
    for (int i = 0; i < _gameState.GameConfiguration.WinCondition; i++)
    {
        if (_gameState.GameBoard[_gameState.SlidingGrid.EndRow - i, _gameState.SlidingGrid.StartCol + i] != gamePiece) continue;
        count++;
        Console.WriteLine(count);
        if (count >= _gameState.GameConfiguration.WinCondition)
        {
            return true; // Win found
        }
    }

    return false; // No win found
    }
    // Allow users place one of the pieces that still in their hand
    public static string MakeAMove()
    {
        switch (_gameState.NextMoveBy)
        {
            case EGamePiece.X when Player1Pieces.Count == 0:
                ThrowError("Player X has no pieces left to place.");
                return "E";
            case EGamePiece.O when Player2Pieces.Count == 0:
                ThrowError("Player O has no pieces left to place.");
                return "E";
        }
        do
        {
            Console.Write("Give me coordinates <x,y>:");
            var coordinates = Console.ReadLine()!;
            var coordinateSplit = coordinates.Split(',');

            if (!CheckCoordinates(coordinateSplit)) continue;
            
            var inputX = int.Parse(coordinateSplit[0]);
            var inputY = int.Parse(coordinateSplit[1]);

            try
            {
                if (_gameState.GameBoard[inputX, inputY] != EGamePiece.Empty)
                {
                    ThrowError("This place is already occupied. Choose another one.");
                    continue;
                }
            }

            catch (IndexOutOfRangeException e)
            {
                ThrowError("Piece were placed out of border bounds. Please try again.");
                continue;
            }
            _gameState.GameBoard[inputX, inputY] = _gameState.NextMoveBy;
            SubtractPieces();

            if (CheckWinCondition(_gameState.NextMoveBy))
            {
                Console.Clear();
                Console.WriteLine($"Congratulations {_gameState.NextMoveBy}! You won!");
                return "E";
            }
            
            // flip the next piece
            _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            break;
            
        } while (true);

        return "E";
    }
    
    // Allows user in the begging of the game place the grid on any location of the board
    public void PlaceAGrid(SlidingGrid gridInstance)
    {
        do
        {
            Console.Write("Choose initial grid position by providing coordinates <x,y>:");
            var gridPosition = Console.ReadLine()!;
            var coordinatesGrid = gridPosition.Split(',');

            if (!CheckCoordinates(coordinatesGrid)) continue;
            
            gridInstance.GetGridBounds();
            
            _gameState.SlidingGrid.GridCenterX = int.Parse(coordinatesGrid[0]);
            _gameState.SlidingGrid.GridCenterY = int.Parse(coordinatesGrid[1]);
            
            gridInstance.GetGridBounds();
            
            if (!(_gameState.SlidingGrid.StartRow >= 0 && _gameState.SlidingGrid.EndRow <= _gameState.GameConfiguration.BoardSizeWidth - 1 &&
                  _gameState.SlidingGrid.StartCol >= 0 && _gameState.SlidingGrid.EndRow <= _gameState.GameConfiguration.BoardSizeHeight - 1))
            {
                ThrowError("Invalid coordinates. Grid is out if board bounds!");
                continue;
            }
            break;
        } while (true);

    }
    private static void SubtractPieces()
    {
        if (_gameState.NextMoveBy == EGamePiece.X)
        {
            Player1Pieces.Remove(EGamePiece.X);
            
        }
        else
        {
            Player2Pieces.Remove(EGamePiece.O);
            
        }
    }

    // Allows user to move the grid one spot in any direction
    public static string MoveAGrid()
    {
        do
        {
            Console.Write("Give me a command to move the grid:");
            var userInput = Console.ReadLine();
            if (!CheckMoveCommand(userInput))
            {
                ThrowError($"Invalid command: '{userInput}'. Please try again.");
                continue;
            }
            try
            {
                switch (userInput.ToUpper())
                {
                    case ("R"):
                        _gameState.SlidingGrid.MoveRight();
                        break;
                    case ("L"):
                        _gameState.SlidingGrid.MoveLeft();
                        break;
                    case ("U"):
                        _gameState.SlidingGrid.MoveUp();
                        break;
                    case ("D"):
                        _gameState.SlidingGrid.MoveDown();
                        break;
                    case ("UR"):
                        _gameState.SlidingGrid.MoveUpRight();
                        break;
                    case ("UL"):
                        _gameState.SlidingGrid.MoveUpLeft();
                        break;
                    case ("DR"):
                        _gameState.SlidingGrid.MoveDownRight();
                        break;
                    case ("DL"):
                        _gameState.SlidingGrid.MoveDownLeft();
                        break;
                }
                break;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        } while (true);
        if (CheckWinCondition(_gameState.NextMoveBy))
        {
            Console.Clear();
            Console.WriteLine($"Congratulations {_gameState.NextMoveBy}! You won!");
            return "E";
        }
        _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return "E";
    }
    
    
    // Allows user to change position of already placed piece
    public static string ChangePiecePosition()
    {
        do
        {
            Console.WriteLine();
            Console.WriteLine("You can move one of your pieces that are in the grid to another place in the grid!");
            Console.WriteLine();
            Console.Write("Enter your piece position that you would like to move <x,y>: ");
            var userInput  = Console.ReadLine();
            var pieceCor = userInput.Split(',');
            
            if(!CheckIfPieceInGrid(pieceCor)) continue;
            
            var inputX = int.Parse(pieceCor[0]);
            var inputY = int.Parse(pieceCor[1]);
            EGamePiece pieceToMove;

            if (_gameState.GameBoard[inputX, inputY] == _gameState.NextMoveBy)
            {
                pieceToMove = _gameState.GameBoard[inputX, inputY];
            }else
            {
                ThrowError($"You can move only your pieces that are in the grid! Your chose was: {_gameState.GameBoard[inputX, inputY]}");
                continue;
            }
            
            Console.Write("Enter new position <x,y> for the piece you want to move: ");
            var newPos = Console.ReadLine();
            pieceCor = newPos.Split(',');
            
            if(!CheckIfPieceInGrid(pieceCor)) continue;
            
            var inputNewX = int.Parse(pieceCor[0]);
            var inputNewY = int.Parse(pieceCor[1]);
            
            if (_gameState.GameBoard[inputNewX, inputNewY] != EGamePiece.Empty)
            {
                ThrowError($"You can place a piece only on empty cells. Your chose was: {_gameState.GameBoard[inputNewX, inputNewY]}");
                continue;
            }
            
            
            _gameState.GameBoard[inputX, inputY] = EGamePiece.Empty;
            _gameState.GameBoard[inputNewX, inputNewY] = pieceToMove;
            
            if (CheckWinCondition(_gameState.NextMoveBy))
            {
                Console.Clear();
                Console.WriteLine($"Congratulations {_gameState.NextMoveBy}! You won!");
                return "E";
            }
            
            _gameState.NextMoveBy = _gameState.NextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            return "E";
            
        } while (true);
    }
    
    // Reset game
    public void ResetGame()
    {
        var copyOffBoard = new EGamePiece[_gameState.GameBoard.GetLength(0), _gameState.GameBoard.GetLength(1)];
        _gameState.NextMoveBy = EGamePiece.X;
        
    }
    
    // Check if user provided the valid coordinates 
    private static bool CheckCoordinates(string[] coordinates)
    {
        if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out var x)
                                    || !int.TryParse(coordinates[1], out var y))
        {
            ThrowError($"Invalid input: '{string.Join(',', coordinates)}'. Please try again. " +
                       $"Coordinates should be in format x,y");
            return false;
        }
        return true;
    }
    
    // Check if piece inside the grid
    private static bool CheckIfPieceInGrid(string[] coordinates)
    {
        if(!CheckCoordinates(coordinates)) return false;
        
        var inputX = int.Parse(coordinates[0]);
        var inputY = int.Parse(coordinates[1]);

        if (_gameState.SlidingGrid.StartRow <= inputX && _gameState.SlidingGrid.EndRow >= inputX
                                                       && _gameState.SlidingGrid.StartCol <= inputY && _gameState.SlidingGrid.EndCol >= inputY)
        {
            return true;
        }
        
        ThrowError($"Invalid input: '{string.Join(',', coordinates)}'. Please try again. " +
                   $"You can't take or place a piece out of bounds of the grid.");
        
        return false;
    }
    
    // Check if move command is valid
    private static bool CheckMoveCommand(string? command)
    {
        if (command == null) return false;
        
        string[] commands = ["R", "L", "U", "D", "UR", "UL", "DR", "DL"];
        
        return commands.Contains(command.ToUpper());

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