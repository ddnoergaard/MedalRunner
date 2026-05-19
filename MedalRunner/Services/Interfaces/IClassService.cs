namespace MedalRunner.Services.Interfaces
{
    public interface IClassService
    {
        Task<string> GetClassNameOnId(int id);
    }
}
