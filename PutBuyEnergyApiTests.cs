using EnsekAutomationFramework.ApiHandler;
using EnsekAutomationFramework.TestHelpers;

namespace EnsekAutomationFramework
{
    [TestClass]
    public class PutBuyEnergyApiTests
    {
        private string baseAddress = "https://qacandidatetest.ensek.io";

        private RestApiHandler _restApiHandler;

        [TestInitialize]
        public void TestIntialize()
        {
            _restApiHandler = new RestApiHandler(baseAddress);
        }

        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(3, 3)]
        [DataRow(4, 4)]
        public async Task Put_Successfully_Buy_Energy_Quanity(int energyId, int quantityToBuy)
        {
            //ACT
            var energyPurchaseResponse = await _restApiHandler.PutBuyEnergyQuantityAsync(energyId, quantityToBuy);

            //ASSERT
            Assert.IsNotNull(energyPurchaseResponse);

            Assert.AreEqual(200, energyPurchaseResponse.statusCode);

            Assert.AreEqual("ok", energyPurchaseResponse.statusDescription.ToLower());

            Assert.IsTrue(OrderIdHelper.FindOrderId(energyPurchaseResponse.buyEnergyQuantityResponseModel.message) != string.Empty);
        }

        [TestMethod]
        public async Task Put_Successfully_Show_insufficient_Nuclear_Fuel_Message()
        {
            //ARRANGE
            int energyId = 2;

            int quantityToBuy = 2;

            string expectedMessage = "There is no nuclear fuel to purchase!";

            //ACT
            var energyPurchaseResponse = await _restApiHandler.PutBuyEnergyQuantityAsync(energyId, quantityToBuy);

            //ASSERT
            Assert.IsNotNull(energyPurchaseResponse);

            Assert.AreEqual(200, energyPurchaseResponse.statusCode);

            Assert.AreEqual("ok", energyPurchaseResponse.statusDescription.ToLower());

            Assert.AreEqual(expectedMessage.ToLower(), energyPurchaseResponse.buyEnergyQuantityResponseModel.message.ToLower());
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _restApiHandler.Dispose();
        }
    }
}
