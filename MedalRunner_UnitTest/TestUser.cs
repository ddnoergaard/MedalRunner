using MedalRunner.Models;
using MedalRunner.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedalRunner_UnitTest
{
    [TestClass]
    public class TestUser
    {
        User user;
        UserRepository userRepo;

        [TestInitialize]
        public async Task BeforeTest()
        {
            user = new User()
            {
                Id = 4,
                FirstName = "Mock",
                LastName = "User",
                Email = "MockUser@Mock.dk",
                Password = "1234",
                RoleId = 2,
                SubscriptionId = 1,
                CreatedAt = Convert.ToDateTime("2026-05-19")
            };

            userRepo = new UserRepository("Data Source= mssql4.unoeuro.com, 1433 ;Initial Catalog=danieldn_dk_db_medal_runner;Persist Security Info=True;User ID=danieldn_dk;Password=n4fA9F3tEpc6dGehyzDb;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;");
        }

        [TestMethod]
        public async Task TestGetUser()
        {
            //Arrange
            User expectedUser = user;

            //GetUser from db in act stage
            User actualUser = await userRepo.GetUserByEmail(user.Email);

            //Assert that expectedName is equal to actual FirstName
            Assert.AreEqual(expectedUser.Id, actualUser.Id);
        }

        [TestMethod]
        //[ExpectedException(typeof())]
        public void TestUserRestrictionName()
        {
            //Act on object
            user.FirstName = "a";
        }
    }
}
