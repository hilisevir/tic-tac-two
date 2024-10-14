namespace GameBrain;

public class TicTacTwoBrain
{
    private EGamePiece[,] _gameBoard;
    
    private EGamePiece _nextMoveBy { get; set; } = EGamePiece.X;

    private readonly GameConfiguration _gameConfiguration;
    
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

    public void MakeAMove()
    {
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
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This place is already occupied. Choose another one.");
                    Console.WriteLine();
                    Console.ResetColor();
                    continue;
                }
            }

            catch (IndexOutOfRangeException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Piece were placed out of border bounds. Please try again.");
                Console.WriteLine();
                Console.ResetColor();
                continue;
            }
            _gameBoard[inputX, inputY] = _nextMoveBy;
            // flip the next piece
            _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
            break;
            
        } while (true);
    }
    
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
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid coordinates. Grid is out if board bounds!");
                Console.WriteLine();
                Console.ResetColor();
                continue;
            }
            break;
        } while (true);

    }

    public void MoveAGrid(SlidingGrid gridConstruct)
    {
        do
        {
            Console.Write("Give me a command to move the grid:");
            var userInput = Console.ReadLine();
            if (!CheckMoveCommand(userInput))
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Invalid command: '{userInput}'. Please try again.");
                Console.WriteLine();
                Console.ResetColor();
                continue;
            }
            try
            {
                switch (userInput.ToUpper())
                {
                    case ("R"):
                        Console.WriteLine("Here");
                        gridConstruct.MoveRight();
                        break;
                    case ("L"):
                        gridConstruct.MoveLeft();
                        break;
                    case ("U"):
                        gridConstruct.MoveUp();
                        break;
                    case ("D"):
                        gridConstruct.MoveDown();
                        break;
                    case ("UR"):
                        gridConstruct.MoveUpRight();
                        break;
                    case ("UL"):
                        gridConstruct.MoveUpLeft();
                        break;
                    case ("DR"):
                        gridConstruct.MoveDownRight();
                        break;
                    case ("DL"):
                        gridConstruct.MoveDownLeft();
                        break;
                }
                break;
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
            }
        } while (true) ;
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

    private bool CheckMoveCommand(string? command)
    {
        if (command == null) return false;
        
        string[] commands = ["R", "L", "U", "D", "UR", "UL", "DR", "DL"];
        
        return commands.Contains(command.ToUpper());

    }
    
    public void ResetGame()
    {
        var copyOffBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;
        
    }
}