using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AttendanceTracker_Web.Models;
using AttendanceTracker_Web.Models.DB;

namespace AttendanceTracker_Web.Tests.Models
{
    [TestClass]
    public class AuthorizationManagerTest
    {
        AuthorizationManager authManager;
        DataBaseFactory dbFactory;
        Account genericAccount1;
        Account genericAccount2;
        AccessToken genericAccessToken1;
        AccessToken genericAccessToken2;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Default"].ConnectionString;

        [TestInitialize]
        public void Setup()
        {
            authManager = new AuthorizationManager();
            dbFactory = new DataBaseFactory();

            genericAccount1 = dbFactory.Account(0, "jdoe@a.com", "a;klsdjf", "a;ldskf");
            genericAccount2 = dbFactory.Account(0, "jdoe@a.com", "jkl;", "a90dfs");
            AddDBTestAccount(ref genericAccount1);
            AddDBTestAccount(ref genericAccount2);

            var token1 = Guid.NewGuid().ToString();
            var token2 = Guid.NewGuid().ToString();
            var now = DateTime.Now;
            var date1 = new DateTime(now.Year, now.Month, now.Day);
            var date2 = new DateTime(now.Year, now.Month, now.Day - 1);
            genericAccessToken1 = dbFactory.AccessToken(genericAccount1.ID, token1, 100000, date1, true);
            genericAccessToken2 = dbFactory.AccessToken(genericAccount2.ID, token2, 1, date1, true);
            AddDBTestAccessToken(genericAccessToken1);
            AddDBTestAccessToken(genericAccessToken2);
        }

        void AddDBTestAccessToken(AccessToken accessToken)
        {
            try
            {
                var queryString = "exec Access_Tokens_AddAccess_Token @user_id, @token, @expires_in, @issued, @does_expire;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@user_id", accessToken.UserID);
                query.AddParameter("@token", accessToken.Token);
                query.AddParameter("@expires_in", accessToken.ExpiresIn);
                query.AddParameter("@issued", accessToken.Issued);
                query.AddParameter("@does_expire", accessToken.DoesExpire);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void AddDBTestAccount(ref Account account)
        {
            try
            {
                var queryString = "exec Accounts_AddAccount @username, @password, @salt;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@username", account.username);
                query.AddParameter("@password", account.password);
                query.AddParameter("@salt", account.salt);
                account.ID = (long)query.ExecuteScalar();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            RemoveDBTestAccessToken(genericAccessToken1);
            RemoveDBTestAccessToken(genericAccessToken2);
            RemoveDBTestAccount(genericAccount1.ID);
            RemoveDBTestAccount(genericAccount2.ID);
        }

        void RemoveDBTestAccessToken(AccessToken accessToken)
        {
            try
            {
                var queryString = "exec Access_Tokens_RemoveAccess_Token @token;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@token", accessToken.Token);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        void RemoveDBTestAccount(long id)
        {
            try
            {
                var queryString = "exec Accounts_RemoveAccount @id;";
                var query = new Query(queryString, connectionString);
                query.AddParameter("@id", id);
                query.ExecuteQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [TestMethod]
        public void IsAuthorized()
        {
            var isAuthorized = authManager.IsAuthorized(genericAccessToken1.Token);
            Assert.IsTrue(isAuthorized);
        }

        [TestMethod]
        public void IsNotAuthorized()
        {
            var isAuthorized = authManager.IsAuthorized(genericAccessToken2.Token);
            Assert.IsFalse(isAuthorized);
        }
    }
}
