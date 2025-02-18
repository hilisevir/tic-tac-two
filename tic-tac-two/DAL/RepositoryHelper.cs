namespace DAL;

public static class RepositoryHelper
{
    private static readonly AppDbContextFactory Factory = new AppDbContextFactory();
    private static readonly AppDbContext DbContext = Factory.CreateDbContext(Array.Empty<string>());
    public static readonly IGameRepository GameRepository = new GameRepositoryDb(DbContext);
    public static readonly IConfigRepository ConfigRepository = new ConfigRepositoryDb(DbContext);
    
    // public static readonly IGameRepository GameRepository = new GameRepositoryJson();
    // public static readonly IConfigRepository ConfigRepository = new ConfigRepositoryJson();
}