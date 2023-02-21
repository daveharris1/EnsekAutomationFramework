using ApiHelper;
using ApiHelper.RequestModels;
using ApiHelper.ResponseModels;

namespace EnsekAutomationFramework
{
    [TestClass]
    public class PostResetDataApiTests
    {
        private string baseAddress = "https://qacandidatetest.ensek.io";

        private ApiHandler _apiHandler;

        private PostLoginResponseModel loginResponse;

        [TestInitialize]
        public void TestIntialize()
        {
            _apiHandler = new ApiHandler(baseAddress);        
        }

        [TestMethod]
        public async Task Post_Reset_Data_Successfully()
        {
            //ARRANGE
            var loginRequest = new PostLoginRequestModel
            {
                username = "test",
                password = "testing"
            };

            //ACT
            loginResponse = await _apiHandler.PostLoginAsync(loginRequest);

            var resetDataResponse = await _apiHandler.PostResetDataAsync("Bearer " + loginResponse.loginResponseModel.access_token);

            //ASSERT
            Assert.IsNotNull(resetDataResponse);

            Assert.AreEqual(200, resetDataResponse.statusCode);

            Assert.AreEqual("ok", resetDataResponse.statusDescription.ToLower());

            Assert.AreEqual("success", resetDataResponse.resetDataResponseModel.message.ToLower());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _apiHandler.Dispose();
        }
    }
}
