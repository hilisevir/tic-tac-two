using Domain;
using GameBrain;

namespace DAL;

public interface IConfigRepository
{
    Dictionary<int, string> GetConfigurationNames();
    GameConfiguration GetGameConfigurationById(int configId);
    Configuration GetConfigurationById(int configId);
    void SaveConfiguration(GameConfiguration newConfig);
    void DeleteConfiguration(GameConfiguration config);
}