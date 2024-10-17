using GameBrain;

namespace DAL;

public class GameRepositoryJson : IGameRepository
{
    
    private readonly string _basePath = Environment
                                            .GetFolderPath(Environment.SpecialFolder.ApplicationData)
                                        + Path.DirectorySeparatorChar + "tic-tac-two" + Path.DirectorySeparatorChar;
    
    public void SaveGame(GameState state)
    {
        var stateJsonStr = System.Text.Json.JsonSerializer.Serialize(state);
        var fileName = state.GameConfiguration.Name + 
                       state.GameConfiguration.Name + " " + 
                       DateTime.Now.ToString("O") + 
                       FileHelper.GameExtension;
        
        File.WriteAllText(fileName, stateJsonStr);
        
    }
}