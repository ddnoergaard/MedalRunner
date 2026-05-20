namespace MedalRunner.Repositories.Interfaces
{
    public interface IClassRepository
    {
        Task<string> GetClassNameOnId(int id);
    }
}
