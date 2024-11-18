﻿using DAL;
using GameBrain;
using MenuSystem;

namespace ConsoleApp;



public static class GameController
{
    private static AppDbContextFactory factory = new AppDbContextFactory();
    private static AppDbContext dbContext = factory.CreateDbContext(Array.Empty<string>());
    private static IConfigRepository ConfigRepository = new ConfigRepositoryDb(dbContext);
    private static readonly IGameRepository GameRepository = new GameRepositoryDb(dbContext);
    
    
    // private static readonly IConfigRepository ConfigRepository = new ConfigRepositoryJson();
    // private static readonly IGameRepository GameRepository = new GameRepositoryJson();
    
    public static string MainLoop()
    {
        Console.Write("Would you like to load the game or start the new one? (L/N): ");
        var choice = Console.ReadLine() ?? "N";
        
        GameConfiguration chosenConfig = default;
        GameState chosenGame = default;
        SlidingGrid gridConstruct;
        TicTacTwoBrain gameInstance;
        
        if (choice.ToUpper() == "N")
        {
            var chosenConfigShortcut = ChooseConfiguration();

            if (!int.TryParse(chosenConfigShortcut, out var configNo))
            {
                return chosenConfigShortcut;
            }
        
            chosenConfig = ConfigRepository.GetConfigurationByName(
                ConfigRepository.GetConfigurationNames()[configNo]
            );
        }
        else
        {
            var chosenGameShortcut = ChooseSavedGame();
            if (!int.TryParse(chosenGameShortcut, out var gameNo))
            {
                return chosenGameShortcut;
            }
            
            chosenGame = GameRepository.GetGameByName(
                GameRepository.GetGameNames()[gameNo]
            );
            
        }


        if (chosenGame != null)
        {
            gridConstruct = new SlidingGrid(
                chosenGame.SlidingGrid.GridCenterX,
                chosenGame.SlidingGrid.GridCenterY,
                chosenGame.SlidingGrid.StartRow,
                chosenGame.SlidingGrid.EndRow,
                chosenGame.SlidingGrid.StartCol,
                chosenGame.SlidingGrid.EndCol);
            gameInstance = new TicTacTwoBrain(chosenGame, gridConstruct);
            
        } 
        else
        {
            gridConstruct = new SlidingGrid(chosenConfig);
            gameInstance = new TicTacTwoBrain(chosenConfig, gridConstruct);
            gameInstance.PlaceAGrid(gridConstruct);
        }
        
        
        var flag = true;
        do
        {
            ConsoleUI.Visualizer.DrawBoard(gameInstance, gridConstruct);
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Place Piece - Place a new piece on the board.");
            Console.WriteLine("2. Move Piece - Move an existing piece to a different position inside the grid.");
            Console.WriteLine("3. Move Grid - Shift the entire grid to a new position.");
            Console.WriteLine("4. Save Game - Save current game.");
            Console.WriteLine("5. Exit");
            Console.Write("Enter the number of your choice: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter the coordinates <x,y> to place a piece: ");
                    input = Console.ReadLine();
                    gameInstance.MakeAMove(input);
                    break;
                case "2":
                    Console.WriteLine("You can move one of your pieces that are in the grid to another place in the grid!");
                    Console.Write("Enter your piece position that you would like to move <x,y>: ");
                    input = Console.ReadLine();
                    gameInstance.ChangePiecePosition(input);
                    break;
                case "3":
                    Console.WriteLine("Give me a command to move the grid:");
                    Console.WriteLine("Use the following commands:");
                    Console.WriteLine("U  - Move Up");
                    Console.WriteLine("D  - Move Down");
                    Console.WriteLine("L  - Move Left");
                    Console.WriteLine("R  - Move Right");
                    Console.WriteLine("UR - Move Up-Right");
                    Console.WriteLine("UL - Move Up-Left");
                    Console.WriteLine("DR - Move Down-Right");
                    Console.WriteLine("DL - Move Down-Left");
                    Console.Write("Enter your command: ");
                    input = Console.ReadLine();
                    gameInstance.MoveAGrid(input);
                    break;
                case "4":
                    Console.Write("Are you sure you want to save the current game? y/n: ");
                    input = Console.ReadLine() ?? "n";
                    if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        Console.Write("Please, enter the name for the game or hit enter to use generic name: ");
                        input = Console.ReadLine();
                        
                        if (string.IsNullOrEmpty(input)) input = GetUniqueGameName();
                        
                        GameRepository.SaveGame(gameInstance.GetGameState(input));
                    }
                    break;
                case "5":
                    Console.Write("Are you sure you want to exit? y/n: ");
                    input = Console.ReadLine() ?? "n";
                    if (input.Equals("y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        flag = false;
                    }
                    break;
            }       
            

        } while (flag);

        return "Game finished!";
    }

    private static string ChooseConfiguration()
    {
        var configMenuItems = new List<MenuItem>();
        for (var i = 0; i < ConfigRepository.GetConfigurationNames().Count; i++)
        {
            var returnValue = i.ToString();
            configMenuItems.Add(new MenuItem()
            {
                Title = ConfigRepository.GetConfigurationNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
    
        var configMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Config", 
            configMenuItems, 
            isCustomMenu: true);

        return configMenu.Run();
    }
    private static string ChooseSavedGame()
    {
        var gameMenuItems = new List<MenuItem>();
        for (var i = 0; i < GameRepository.GetGameNames().Count; i++)
        {
            var returnValue = i.ToString();
            gameMenuItems.Add(new MenuItem()
            {
                Title = GameRepository.GetGameNames()[i],
                Shortcut = (i + 1).ToString(),
                MenuItemAction = () => returnValue
            });
        }
    
        var loadMenu = new Menu(EMenuLevel.Secondary, "TIC-TAC-TWO Game Saves", 
            gameMenuItems, 
            isCustomMenu: true);

        return loadMenu.Run();
    }
    private static string GetUniqueGameName()
    {
        var count = 0;
        var res = GameRepository.GetGameNames();
        for (int i = 0; i < res.Count; i++)
        {
            if (res[i].Contains("New Save "))
            {
                count++;
            }
        }
        return "New Save " + (count + 1);
    }
}