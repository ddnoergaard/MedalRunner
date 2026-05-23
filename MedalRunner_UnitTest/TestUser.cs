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

        [TestMethod]
        public void TestUserRestrictionEmail()
        {
            //Arrange 
            user.Email = "";

            //Asssert to check if Email restriction is correct 
            Assert.IsTrue(ValidateModel(user).Any(
            v =>   
                    v.ErrorMessage.Contains("Email must not be empty")));
        }

        [TestMethod]
        public void TestUserFirstNameEmptyRestriction()
        {
            //Arrange
            user.FirstName = "";

            //Assert to check for FirstName restrictions
            Assert.IsTrue(ValidateModel(user).Any(
                v => v.ErrorMessage.Contains("Must insert a first name")));
        }

        [TestMethod]
        public void TestUserLastNameEmptyRestriction()
        {
            //Arrange
            user.LastName = "";

            //Assert to check for LastName restrictions
            Assert.IsTrue(ValidateModel(user).Any(
                v => v.ErrorMessage.Contains("Must insert a last name")));
        }

        [TestMethod]
        public void TestUserFirstNameRestriction()
        {
            //Arrange
            user.FirstName = "i";

            //Assert to check for LastName restrictions
            Assert.IsTrue(ValidateModel(user).Any(
                v => v.ErrorMessage.Contains("The minimum character count is 2")));
        }

        [TestMethod]
        public void TestUserLastNameRestriction()
        {
            //Arrange
            user.LastName = "a";

            //Assert to check for LastName restrictions
            Assert.IsTrue(ValidateModel(user).Any(
                v => v.ErrorMessage.Contains("The minimum character count is 2")));
        }

        [TestMethod]
        public void TestUserPasswordRestriction()
        {
            //Arrange
            user.Password = "ab";

            //Assert to check for LastName restrictions
            Assert.IsTrue(ValidateModel(user).Any(
                v => v.ErrorMessage.Contains("Password must have at least 3 characters")));
        }
        //Method pulled from stackOverFlow to validate data annotations
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
