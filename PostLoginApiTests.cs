﻿using ApiHelper;
using ApiHelper.RequestModels;

namespace EnsekAutomationFramework
{
    [TestClass]
    public class PostLoginApiTests
    {
        private string baseAddress = "https://qacandidatetest.ensek.io";

        private ApiHandler _apiHandler;

        [TestInitialize]
        public void TestIntialize()
        {
            _apiHandler = new ApiHandler(baseAddress);
        }

        [TestMethod]
        public async Task Post_Valid_Login_Data_Returns_Successfully()
        {
            //ARRANGE
            var loginRequest = new PostLoginRequestModel
            {
                username = "test",
                password = "testing"
            };

            //ACT
            var loginResponse = await _apiHandler.PostLoginAsync(loginRequest);

            //ASSERT
            Assert.IsNotNull(loginResponse);

            Assert.AreEqual(200, loginResponse.statusCode);

            Assert.AreEqual("ok", loginResponse.statusDescription.ToLower()); ;
            
            Assert.AreEqual("success", loginResponse.loginResponseModel.message.ToLower());

            Assert.IsNotNull(loginResponse.loginResponseModel.access_token);
        }

        [TestMethod]
        public async Task Post_Valid_Login_Data_Returns_UnAuthorized()
        {
            //ARRANGE
            var loginRequest = new PostLoginRequestModel
            {
                username = "invalidUserName",
                password = "invalidPassword"
            };

            //ACT
            var loginResponse = await _apiHandler.PostLoginAsync(loginRequest);

            //ASSERT
            Assert.IsNotNull(loginResponse);

            Assert.AreEqual(401, loginResponse.statusCode);

            Assert.AreEqual("unauthorized", loginResponse.statusDescription.ToLower());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _apiHandler.Dispose();
        }
    }
}
