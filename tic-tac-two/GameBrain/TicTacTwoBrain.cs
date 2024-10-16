namespace GameBrain;

public class TicTacTwoBrain
{
    private static EGamePiece[,] _gameBoard;
    
    private static EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    private static GameConfiguration _gameConfiguration;
    private static SlidingGrid _slidingGrid;
    public static List<EGamePiece> Player1PieceAmount { get; private set; }
    public static List<EGamePiece> Player2PieceAmount { get; private set; }

    public TicTacTwoBrain(GameConfiguration gameConfiguration, SlidingGrid slidingGrid)
    {
        _gameConfiguration = gameConfiguration;
        Player1PieceAmount = _gameConfiguration.Player1Pieces;
        Player2PieceAmount = _gameConfiguration.Player2Pieces;
        _gameBoard = new EGamePiece[_gameConfiguration.BoardSizeHeight, _gameConfiguration.BoardSizeWidth];
        _slidingGrid = slidingGrid;
    }
    
    

    public EGamePiece[,] GameBoard
    {
        get => GetBoard();
        private set => _gameBoard = value;
    }
    
    public int DimX => _gameBoard.GetLength(0);
    public int DimY => _gameBoard.GetLength(1);
    
    private EGamePiece[,] GetBoard()
    {
        var copyOffBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        for (var x = 0; x < _gameBoard.GetLength(0); x++)
        {
            for (var y = 0; y < _gameBoard.GetLength(1); y++)
            {
                copyOffBoard[x, y] = _gameBoard[x, y];
            }
        }
        return copyOffBoard;
    }

    // Allows user place one of the pieces that still in their hand
    public static string MakeAMove()
    {
        switch (_nextMoveBy)
        {
            case EGamePiece.X when Player1PieceAmount.Count == 0:
                ThrowError("Player X has no pieces left to place.");
                return "E";
            case EGamePiece.O when Player2PieceAmount.Count == 0:
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
                if (_gameBoard[inputX, inputY] != EGamePiece.Empty)
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
            _gameBoard[inputX, inputY] = _nextMoveBy;
            SubtractPieces();
            // flip the next piece
            _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
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
            
            
            gridInstance.GridCenterX = int.Parse(coordinatesGrid[0]);
            gridInstance.GridCenterY = int.Parse(coordinatesGrid[1]);
            
            gridInstance.GetGridBounds();
            if (!(gridInstance.StartRow >= 0 && gridInstance.EndRow <= _gameConfiguration.BoardSizeWidth - 1 &&
                  gridInstance.StartCol >= 0 && gridInstance.EndRow <= _gameConfiguration.BoardSizeHeight - 1))
            {
                ThrowError("Invalid coordinates. Grid is out if board bounds!");
                continue;
            }
            break;
        } while (true);

    }
    private static void SubtractPieces()
    {
        if (_nextMoveBy == EGamePiece.X)
        {
            Player1PieceAmount.Remove(EGamePiece.X);
            
        }
        else
        {
            Player2PieceAmount.Remove(EGamePiece.O);
            
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
                        _slidingGrid.MoveRight();
                        break;
                    case ("L"):
                        _slidingGrid.MoveLeft();
                        break;
                    case ("U"):
                        _slidingGrid.MoveUp();
                        break;
                    case ("D"):
                        _slidingGrid.MoveDown();
                        break;
                    case ("UR"):
                        _slidingGrid.MoveUpRight();
                        break;
                    case ("UL"):
                        _slidingGrid.MoveUpLeft();
                        break;
                    case ("DR"):
                        _slidingGrid.MoveDownRight();
                        break;
                    case ("DL"):
                        _slidingGrid.MoveDownLeft();
                        break;
                }
                break;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        } while (true);
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
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

            if (_gameBoard[inputX, inputY] == _nextMoveBy)
            {
                pieceToMove = _gameBoard[inputX, inputY];
            }else
            {
                ThrowError($"You can move only your pieces that are in the grid! Your chose was: {_gameBoard[inputX, inputY]}");
                continue;
            }
            
            Console.Write("Enter new position <x,y> for the piece you want to move: ");
            var newPos = Console.ReadLine();
            pieceCor = newPos.Split(',');
            
            if(!CheckIfPieceInGrid(pieceCor)) continue;
            
            var inputNewX = int.Parse(pieceCor[0]);
            var inputNewY = int.Parse(pieceCor[1]);
            
            if (_gameBoard[inputNewX, inputNewY] != EGamePiece.Empty)
            {
                ThrowError($"You can place a piece only on empty cells. Your chose was: {_gameBoard[inputNewX, inputNewY]}");
                continue;
            }
            
            
            _gameBoard[inputX, inputY] = EGamePiece.Empty;
            _gameBoard[inputNewX, inputNewY] = pieceToMove;
                
            
            _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            return "E";
            
        } while (true);
    }
    
    // Reset game
    public void ResetGame()
    {
        var copyOffBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;
        
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

        if (_slidingGrid.StartRow <= inputX && _slidingGrid.EndRow >= inputX
                                            && _slidingGrid.StartCol <= inputY && _slidingGrid.EndCol >= inputY)
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