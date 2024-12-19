using Domain;

namespace DAL;

public static class GameTypeHardcoded
{
    public static List<GameType> GetInitialGameTypes()
    {
        return
        [
            new GameType
            {
                Id = 1,
                Name = "PvP",
                Description = "Player versus other player."
            },

            new GameType
            {
                Id = 2,
                Name = "PvC",
                Description = "Player versus computer."
            },

            new GameType
            {
                Id = 3,
                Name = "CvC",
                Description = "Computer versus other computer."
            }
        ];
    }
}