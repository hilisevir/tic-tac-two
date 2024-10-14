namespace GameBrain;

public class SlidingGrid
{
    private readonly GameConfiguration _gameConfiguration;
    private readonly int _gridHeight;
    private readonly int _gridWidth;
    private string _gridColor;
    public int GridCenterX { get; set; } = 2;
    public int GridCenterY { get; set; } = 2;

    public int StartRow { get; private set; }
    public int EndRow { get; private set; }
    public int StartCol { get; private set; }
    public int EndCol { get; private set; }
    
    
    public SlidingGrid(GameConfiguration gameConfiguration)
    {
        _gameConfiguration = gameConfiguration;
        _gridColor = _gameConfiguration.GridColor;
        _gridHeight = _gameConfiguration.GridSizeHeight;
        _gridWidth = _gameConfiguration.GridSizeWidth;
    }

    public void GetGridBounds()
    {
        var halfHeight = _gridHeight / 2;
        var halfWidth = _gridWidth / 2;

        StartRow = GridCenterX - halfHeight;
        EndRow = GridCenterX + halfHeight;
        StartCol = GridCenterY - halfWidth;
        EndCol = GridCenterY + halfWidth;
    }
    public void MoveRight()
    {
        if (EndRow + 1 <= _gameConfiguration.BoardSizeWidth - 1)
        {
            GridCenterX++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move right, at the boundary!");
        }
    }

    // Method to move the window left
    public void MoveLeft()
    {
        if (StartRow - 1 >= 0)
        {
            GridCenterX--;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move left, at the boundary!");
        }
    }

    // Method to move the window up
    public void MoveUp()
    {
        if (StartCol - 1 >= 0)
        {
            GridCenterY--;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up, at the boundary!");
        }
    }

    // Method to move the window down
    public void MoveDown()
    {
        if (GridCenterY + 1 <= _gameConfiguration.BoardSizeHeight - 1)
        {
            GridCenterY++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down, at the boundary!");
        }
        
    }public void MoveUpLeft()
    {
        if (StartRow - 1 >= 0 && StartCol - 1 >= 0)
        {
            GridCenterY++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up and left, at the boundary!");
        }
        
    }public void MoveUpRight()
    {
        if (EndRow + 1 <= _gameConfiguration.BoardSizeWidth - 1 && StartCol - 1 >= 0)
        {
            GridCenterY++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up and right, at the boundary!");
        }
        
    }public void MoveDownLeft()
    {
        if (GridCenterY + 1 <= _gameConfiguration.BoardSizeHeight - 1 && StartRow - 1 >= 0)
        {
            GridCenterY++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down and left, at the boundary!");
        }
        
    }public void MoveDownRight()
    {
        if (GridCenterY + 1 <= _gameConfiguration.BoardSizeHeight - 1
            && StartRow + 1 <= _gameConfiguration.BoardSizeWidth - 1)
        {
            GridCenterY++;
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down and right, at the boundary!");
        }
    }
    
}