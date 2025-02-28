﻿using System.Text.Json.Serialization;

namespace GameBrain;

public class SlidingGrid
{
    public GameConfiguration GameConfiguration;
    public int GridHeight { get;}
    public int GridWidth { get;}
    // private string _gridColor;
    public int GridCenterX { get; set; }
    public int GridCenterY { get; set; }

    public int StartRow { get; private set; }
    public int EndRow { get; private set; }
    public int StartCol { get; private set; }
    public int EndCol { get; private set; }
    
    
    
    public SlidingGrid(GameConfiguration gameConfiguration)
    {
        GameConfiguration = gameConfiguration;
        GridHeight = GameConfiguration.GridHeight;
        GridWidth = GameConfiguration.GridWidth;
    }

    [JsonConstructor]
    public SlidingGrid(int gridCenterX, int gridCenterY, int startRow, int endRow, int startCol, int endCol, int gridHeight, int gridWidth)
    {
        GridCenterX = gridCenterX;
        GridCenterY = gridCenterY;
        StartRow = startRow;
        EndRow = endRow;
        StartCol = startCol;
        EndCol = endCol;
        GridHeight = gridHeight;
        GridWidth = gridWidth;
    }
    
    public void GetGridBounds()
    {
        var halfHeight = GridHeight / 2;
        var halfWidth = GridWidth / 2;

        StartRow = GridCenterX - halfWidth;
        EndRow = GridHeight % 2 == 0 ? GridCenterX + halfHeight - 1 : GridCenterX + halfHeight;

        StartCol = GridCenterY - halfHeight;
        EndCol = GridWidth % 2 == 0 ? GridCenterY + halfWidth - 1 : GridCenterY + halfWidth;
    }
    public void MoveRight()
    {
        if (EndRow + 1 <= GameConfiguration.BoardWidth - 1)
        {
            GridCenterX++;
            GetGridBounds();
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
            GetGridBounds();
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
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up, at the boundary!");
        }
    }

    // Method to move the window down
    public void MoveDown()
    {
        if (EndCol + 1 <= GameConfiguration.BoardHeight - 1)
        {
            GridCenterY++;
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down, at the boundary!");
        }
        
    }public void MoveUpLeft()
    {
        if (StartRow - 1 >= 0 && StartCol - 1 >= 0)
        {
            GridCenterY--;
            GridCenterX--;
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up and left, at the boundary!");
        }
        
    }public void MoveUpRight()
    {
        if (EndRow + 1 <= GameConfiguration.BoardWidth - 1 && StartCol - 1 >= 0)
        {
            GridCenterY--;
            GridCenterX++;
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move up and right, at the boundary!");
        }
        
    }public void MoveDownLeft()
    {
        if (EndCol + 1 <= GameConfiguration.BoardHeight - 1 && StartRow - 1 >= 0)
        {
            GridCenterY++;
            GridCenterX--;
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down and left, at the boundary!");
        }
        
    }public void MoveDownRight()
    {
        if (EndCol + 1 <= GameConfiguration.BoardHeight - 1
            && StartRow + 1 <= GameConfiguration.BoardWidth - 1)
        {
            GridCenterY++;
            GridCenterX++;
            GetGridBounds();
        }
        else
        {
            throw new IndexOutOfRangeException("Cannot move down and right, at the boundary!");
        }
    }
    
}