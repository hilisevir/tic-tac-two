namespace GameBrain;

public class SlidingGrid
{
    private GameConfiguration _gameConfiguration;
    private int _gridHeight;
    private int _gridWidth;
    private string _gridColor;
    public int GridCenterX { get; set; } = 3;
    public int GridCenterY { get; set; } = 3;

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

        int startRow = GridCenterX - halfHeight;
        int endRow = GridCenterX + halfHeight;
        int startCol = GridCenterY - halfWidth;
        int endCol = GridCenterY + halfWidth;

        return (startRow, endRow, startCol, endCol);
    }
    public void MoveRight()
    {
        if (GridCenterX + 1 < _gameConfiguration.BoardSizeWidth - 1)
        {
            GridCenterX++;
        }
        else
        {
            Console.WriteLine("Cannot move right, at the boundary!");
        }
    }

    // Method to move the window left
    public void MoveLeft()
    {
        if (GridCenterX - 1 >= 0)
        {
            GridCenterX--;
        }
        else
        {
            Console.WriteLine("Cannot move left, at the boundary!");
        }
    }

    // Method to move the window up
    public void MoveUp()
    {
        if (GridCenterY - 1 >= 0)
        {
            GridCenterY--;
        }
        else
        {
            Console.WriteLine("Cannot move up, at the boundary!");
        }
    }

    // Method to move the window down
    public void MoveDown()
    {
        if (GridCenterY + 1 < _gameConfiguration.BoardSizeHeight - 1)
        {
            GridCenterY++;
        }
        else
        {
            Console.WriteLine("Cannot move down, at the boundary!");
        }
    }

    
    
}