using System.Text.Json;
using GameBrain;

namespace DAL;

public class ConfigRepositoryJson : IConfigRepository
{ 
    public List<string> GetConfigurationNames()
    {
        ChecckAndCreateInitialConfig();
        
        var res = new List<string>();
        foreach (var fullFileName in Directory.GetFiles(FileHelper._basePath, "*" + FileHelper.ConfigExtension))
        {
            var filenameParts = Path.GetFileNameWithoutExtension(fullFileName);
            var primaryName = Path.GetFileNameWithoutExtension(filenameParts);
            res.Add(primaryName);
        }

        return res;
    }

    private void ChecckAndCreateInitialConfig()
    {
        if (!Directory.Exists(FileHelper._basePath))
        {
            Directory.CreateDirectory(FileHelper._basePath);
        }
        var data = Directory.GetFiles(FileHelper._basePath, "*" + FileHelper.ConfigExtension).ToList();
        if (data.Count == 0)
        {
            var hardCodedRepo = new ConfigRepositoryHardcoded();
            var optionNames = hardCodedRepo.GetConfigurationNames();
            foreach (var optionName in optionNames)
            {
                var gameOption = hardCodedRepo.GetConfigurationByName(optionName);
                var optionJsonStr = JsonSerializer.Serialize(gameOption);
                File.WriteAllText(FileHelper._basePath + gameOption.Name + "*" + FileHelper.ConfigExtension, optionJsonStr);
            }
        }
    }

    public GameConfiguration GetConfigurationByName(string name)
    {
        var configJsonStr = File.ReadAllText(FileHelper._basePath + name + FileHelper.ConfigExtension);
        var config = JsonSerializer.Deserialize<GameConfiguration>(configJsonStr);
        return config;
    }

    public void SaveConfiguration(GameConfiguration config)
    {
        var configJsonStr = JsonSerializer.Serialize(config);
        var timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss");
        var fileName = FileHelper._basePath + 
                       config.Name + " " + 
                       timestamp + 
                       FileHelper.ConfigExtension;
        
        File.WriteAllText(fileName, configJsonStr);
    }
}