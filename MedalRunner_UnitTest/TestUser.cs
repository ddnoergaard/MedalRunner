using MedalRunner.Models;
using MedalRunner.Repositories;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //[TestMethod]
        //public void TestUserRestrictionName(string )
        //{
            

        //    //Act on object
        //    Assert.IsTrue(ValidateModel(user).Any(
        //    v => !v.MemberNames.Contains("Email") &&
        //         !v.ErrorMessage.Contains("Email must not be empty")));
        //}

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
