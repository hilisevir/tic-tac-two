namespace GameBrain;

public class GameState
{
    public EGamePiece[,] GameBoard;
    
    public EGamePiece NextMoveBy { get; set; } = EGamePiece.X;

    public GameConfiguration GameConfiguration;
    public SlidingGrid SlidingGrid;
    public List<EGamePiece> Player1PieceAmount { get; private set; }
    public List<EGamePiece> Player2PieceAmount { get; private set; }

    public GameState(EGamePiece[,] gameBoard, GameConfiguration gameConfiguration, SlidingGrid slidingGrid)
    {
        SlidingGrid = slidingGrid;
        GameBoard = gameBoard;
        GameConfiguration = gameConfiguration;
        Player1PieceAmount = gameConfiguration.Player1Pieces;
        Player2PieceAmount = gameConfiguration.Player2Pieces;
    }
}