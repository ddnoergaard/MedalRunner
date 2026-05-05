using MedalRunner.Repositories.Interfaces;

namespace MedalRunner.Services.Interfaces;

public class DungeonService : IDungeonService
{
    private readonly IDungeonRepository _dungeonRepository;

    public DungeonService(IDungeonRepository dungeonRepository)
    {
        _dungeonRepository = dungeonRepository;
    }
}
