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
        DungeonRepository _dungeonRepo = new DungeonRepository("Data Source= mssql4.unoeuro.com, 1433 ;Initial Catalog=danieldn_dk_db_medal_runner;Persist Security Info=True;User ID=danieldn_dk;Password=n4fA9F3tEpc6dGehyzDb;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");

        
        

        [TestInitialize]
        public async Task BeforeTestAsync()
        {
            List<Dungeon> dungeons;
            dungeons = await _dungeonRepo.GetAllDungeonsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task TestDungeonByIdException1()
        {
            await _dungeonRepo.GetDungeonByIdAsync(30);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
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
