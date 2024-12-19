using System.Text.Json;
using Domain;
using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{ 
    public Dictionary<int, string> GetConfigurationNames()
    {
        CheckAndCreateInitialConfig();
        
        var res = new Dictionary<int, string>();
        foreach (var fullFileName in Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.ConfigExtension))
        {
            var json = File.ReadAllText(fullFileName);
            
            var gameConfig = JsonSerializer.Deserialize<GameConfiguration>(json);
            
            res.Add(gameConfig.Id, gameConfig.Name);
        }

        return res;
    }

    private static void CheckAndCreateInitialConfig()
    {
        if (!Directory.Exists(FileHelper.BasePath))
        {
            Directory.CreateDirectory(FileHelper.BasePath);
        }
        
        var data = Directory.GetFiles(FileHelper.BasePath, "*" + FileHelper.ConfigExtension).ToList();

        if (data.Count != 0) return;
        
        var hardCodedRepo = new ConfigRepositoryHardcoded();
        var optionNames = hardCodedRepo.GetConfigurationNames();
        foreach (var optionName in optionNames)
        {
            var gameOption = hardCodedRepo.GetGameConfigurationById(optionName.Key);
            var optionJsonStr = JsonSerializer.Serialize(gameOption);

            // Ensure the file name is valid
            var fileName = Path.Combine(FileHelper.BasePath, gameOption.Name + FileHelper.ConfigExtension);
            
            File.WriteAllText(fileName, optionJsonStr);
        }
        
        if (!File.Exists(FileHelper.ConfigIdCounterFile))
        {
            File.WriteAllText(FileHelper.ConfigIdCounterFile, optionNames.Count.ToString());
        }
    }


    public GameConfiguration GetGameConfigurationById(int configId)
    {
        var configDict = GetConfigurationNames();
        var configJsonStr = File.ReadAllText(FileHelper.BasePath + configDict[configId] + FileHelper.ConfigExtension);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        return config;
    }

    public Configuration GetConfigurationById(int configId)
    {
        var configDict = GetConfigurationNames();
        var configJsonStr = File.ReadAllText(FileHelper.BasePath + configDict[configId] + FileHelper.ConfigExtension);
        var config = JsonSerializer.Deserialize<Configuration>(configJsonStr);
        return config ?? throw new InvalidOperationException();
    }

    public void SaveConfiguration(GameConfiguration config)
    {
        config.Id = GetNextId();
        
        var configJsonStr = JsonSerializer.Serialize(config);
        var fileName = FileHelper.BasePath + 
                       config.Name +
                       FileHelper.ConfigExtension;
        
        File.WriteAllText(fileName, configJsonStr);
    }

    public void DeleteConfiguration(GameConfiguration config)
    {
        var filePath = Path.Combine(FileHelper.BasePath, config.Name + FileHelper.ConfigExtension);
        
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        else
        {
            Console.WriteLine($"Configuration '{config.Name}' not found. Nothing to delete.");
        }
    }

    private static int GetNextId()
    {
        if (!File.Exists(FileHelper.ConfigIdCounterFile))
        {
            CheckAndCreateInitialConfig();
        }
        
        var currentId = int.Parse(File.ReadAllText(FileHelper.ConfigIdCounterFile));
        
        var nextId = currentId + 1;
        File.WriteAllText(FileHelper.ConfigIdCounterFile, nextId.ToString());

        return nextId;
    }
}