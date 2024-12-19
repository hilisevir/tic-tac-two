using Domain;
using GameBrain;

namespace DAL;

public interface IGameRepository
{
    Dictionary<int, string> GetGameNames();
    void SaveGame(GameState gameState);
    GameState? GetGameStateById(int gameId);
    void DeleteGame(int gameId);
}