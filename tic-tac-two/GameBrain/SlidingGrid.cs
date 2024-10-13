namespace GameBrain;

public class SlidingGrid
{
    private GameConfiguration _gameConfiguration;
    private int _gridHeight;
    private int _gridWidth;
    private string _gridColor;
    public int GridCenterX { get; set; } = 2;
    public int GridCenterY { get; set; } = 2;

    // Pass GameConfiguration as a parameter
    public SlidingGrid(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gridColor = _gameConfiguration.GridColor;
        _gridHeight = _gameConfiguration.GridSizeHeight;
        _gridWidth = _gameConfiguration.GridSizeWidth;
    }

    public (int startRow, int endRow, int startCol, int endCol) GetGridBounds()
    {
        int halfHeight = _gridHeight / 2;
        int halfWidth = _gridWidth / 2;

        int startRow = Math.Max(GridCenterX - halfHeight, 0);
        int endRow = Math.Min(GridCenterX + halfHeight, _gameConfiguration.BoardSizeWidth - 1);
        int startCol = Math.Max(GridCenterY - halfWidth, 0);
        int endCol = Math.Min(GridCenterY + halfWidth, _gameConfiguration.BoardSizeHeight - 1);

        return (startRow, endRow, startCol, endCol);
    }
    
}