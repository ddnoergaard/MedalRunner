using MedalRunner.Models;
using MedalRunner.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using System.Security;

namespace MedalRunner_UnitTest;

[TestClass]
public class TestUserRepo
{
    private UserRepository _repo;
    private User _testUser;
    private string _testEmail = "testuser@medalrunner.dk";


    [TestInitialize]
    public void Setup()
    {
        string connectionString = "Server=mssql4.unoeuro.com;Database=danieldn_dk_db_medal_runner;User ID=danieldn_dk;Password=n4fA9F3tEpc6dGehyzDb;TrustServerCertificate=True;";
        _repo = new UserRepository(connectionString);

        _testUser = new User("Test", "User", _testEmail, "hashpassword123")
        {
            RoleId = 2,
            SubscriptionId = 1
        };
    }

    [TestCleanup]
    public async Task Cleanup()
    {
        try
        {
            User user = await _repo.GetUserByEmail(_testEmail);
            await _repo.DeleteUserById(user.Id);
        } catch (ArgumentException)
        {

        }
    }


    [TestMethod]
    public async Task AddUser_ShouldInsertUserIntoDb()
    {
        await _repo.AddUser(_testUser);

        User result = await _repo.GetUserByEmail(_testEmail);

        Assert.AreEqual(_testEmail, result.Email);
        Assert.AreEqual("Test", result.FirstName);

    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task GetUserByEmail_ThrowArgumentException_WhenUserDoesNotExist()
    {
        await _repo.GetUserByEmail("email@throwargument.com");
    }

    [TestMethod]
    public async Task DeleteUser_ThrowsAgrumentException_WhenUserDoesNotExist()
    {
        await _repo.AddUser(_testUser);
        User tempUser = await _repo.GetUserByEmail(_testUser.Email);
        await _repo.DeleteUserById(tempUser.Id);

        await Assert.ThrowsExceptionAsync<ArgumentException>(
            () => _repo.GetUserByEmail(_testUser.Email)
            );
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public async Task DeleteUserById_ShouldThrowArgumentException_WhenUserDoesNotExist()
    {
        await _repo.DeleteUserById(-1);
    }

    [TestMethod]
    public async Task GetUserCount_ShouldIncrease_WhenUserIsAdded()
    {
        int preCount = 0;
        int postCount = 0;

        preCount = await _repo.GetUserCount();

        await _repo.AddUser(_testUser);

        postCount = await _repo.GetUserCount();

        Assert.AreEqual(preCount + 1, postCount);
    }


}
