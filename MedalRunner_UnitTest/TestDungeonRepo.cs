using MedalRunner.Models;
using MedalRunner.Repositories;
using MedalRunner.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace MedalRunner_UnitTest
{
    [TestClass]
    public sealed class TestDungeonRepo
    {
        private static string conString;
        public TestDungeonRepo(IConfiguration configuration)
        {
            conString = configuration.GetConnectionString("DefaultConnection");
        }

        DungeonRepository _dungeonRepo = new DungeonRepository(conString);

        
        

        [TestInitialize]
        public async Task BeforeTestAsync()
        {
            List<Dungeon> dungeons;
            dungeons = await _dungeonRepo.GetAllDungeonsAsync();
        }   
        
        
        [TestMethod]
        [ExpectedException(typeof(SqlException))]
        public async Task TestDeleteDungeonAsyncException1()
        {
            await _dungeonRepo.DeleteDungeonAsync(100);    
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public async Task TestGetBossesByDungeonIdException1()
        {
            await _dungeonRepo.GetBossesByDungeonIdAsync(50);  
        }
    }
}
