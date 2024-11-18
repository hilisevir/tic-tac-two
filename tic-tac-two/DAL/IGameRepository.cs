using GameBrain;

namespace DAL;

public interface IGameRepository
{
    List<string> GetGameNames();
    public void SaveGame(GameState newGameState);
    GameState GetGameByName(string gameName);
}