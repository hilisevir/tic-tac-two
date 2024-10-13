namespace GameBrain;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard;
    
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    private GameConfiguration _gameConfiguration;
    private SlidingGrid _slidingGrid;
    
    public TicTacTwoBrain(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gameBoard = new EGamePiece[_gameConfiguration.BoardSizeHeight, _gameConfiguration.BoardSizeWidth];
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

    public bool MakeAMove(string coordinates)
    {
        var coordinateSplit = coordinates.Split(',');

        if (!CheckCoordinates(coordinateSplit)) return true;
        
        var inputX = int.Parse(coordinateSplit[0]);
        var inputY = int.Parse(coordinateSplit[1]);
        
        try
        {
            if (_gameBoard[inputX, inputY] != EGamePiece.Empty) return true;
        }
        
        catch (IndexOutOfRangeException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Piece were placed out of border bounds. Please try again.");
            Console.WriteLine();
            Console.ResetColor();
            return true;
        }
        
        _gameBoard[inputX, inputY] = _nextMoveBy;
        // flip the next piece
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return false;
    }
    
    public bool PlaceGrid(SlidingGrid gridConstruct, string userInput)
    {
        var coordinatesGrid = userInput.Split(',');

        if (!CheckCoordinates(coordinatesGrid)) return true;
        
        
        gridConstruct.GridCenterX = int.Parse(coordinatesGrid[0]);
        gridConstruct.GridCenterY = int.Parse(coordinatesGrid[1]);
        
        var (startRow, endRow, startCol, endCol) = gridConstruct.GetGridBounds();
        if (!(startRow >= 0 && endRow <= _gameConfiguration.BoardSizeWidth - 1 &&
              startCol >= 0 && endCol <= _gameConfiguration.BoardSizeHeight - 1))
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid coordinates. Grid is out if board bounds!");
            Console.WriteLine();
            Console.ResetColor();
            return true;
        }
        return false;

    }

    public void ResetGame()
    {
        var copyOffBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;
        
    }

    private static bool CheckCoordinates(string[] coordinates)
    {
        if (coordinates.Length != 2 || !int.TryParse(coordinates[0], out var x)
                                        || !int.TryParse(coordinates[1], out var y))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Invalid input: '{string.Join(',', coordinates)}'. Please try again. " +
                              $"Coordinates should be in format x,y");
            Console.WriteLine();
            Console.ResetColor();
            return false;
        }
        return true;
    }
}