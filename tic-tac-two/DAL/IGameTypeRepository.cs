using Domain;

namespace DAL;

public interface IGameTypeRepository
{
    Dictionary<int, string> GetGameTypeNames();
    void SaveGameType(GameType gameType);
    void CheckAndCreateInitialGameTypes();
    GameType GetGameTypeById(int id);
}