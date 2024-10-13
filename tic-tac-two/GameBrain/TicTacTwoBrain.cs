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

    public bool MakeAMove(int x, int y)
    {

        if (_gameBoard[x, y] != EGamePiece.Empty)
        {
            return false;
        }

        _gameBoard[x, y] = _nextMoveBy;
        // flip the next piece
        _nextMoveBy = _nextMoveBy == EGamePiece.X ? EGamePiece.O : EGamePiece.X;
        return true;
    }

    public void ResetGame()
    {
        var copyOffBoard = new EGamePiece[_gameBoard.GetLength(0), _gameBoard.GetLength(1)];
        _nextMoveBy = EGamePiece.X;
        
    }
}