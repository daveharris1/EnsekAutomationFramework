using ApiHelper;
using ApiHelper.ResponseModels;
using EnsekAutomationFramework.Helpers;
using EnsekAutomationFramework.TestHelperModels;
using EnsekAutomationFramework.TestModels;

namespace EnsekAutomationFramework
{
    [TestClass]
    public class GetOrdersApiTests
    {
        private string baseAddress = "https://qacandidatetest.ensek.io";

        private ApiHandler _apiHandler;

        [TestInitialize]
        public void TestIntialize()
        {
            _apiHandler = new ApiHandler(baseAddress);
        }

        [TestMethod]
        public async Task Get_Previous_Successfully_Made_Energy_Orders()
        {
            //ARRANGE
            List<EnergyOrderModel> energyOrderModels = new List<EnergyOrderModel>

            {
            new EnergyOrderModel {energyId = 1, quantityToBuy = 1 },
            new EnergyOrderModel {energyId = 3, quantityToBuy = 3 },
            new EnergyOrderModel {energyId = 4, quantityToBuy = 4 }
            };

            //ACT
            var successfulEnergyOrders = await CreateEnergyOrders(energyOrderModels);

            var GetOrdersResponse = await _apiHandler.GetOrdersAsync();

            //ASSERT
            Assert.IsNotNull(GetOrdersResponse);

            Assert.AreEqual(200, GetOrdersResponse.statusCode);

            Assert.AreEqual("ok", GetOrdersResponse.statusDescription.ToLower());

            foreach (var successfulEnergyOrder in successfulEnergyOrders)
            {
                foreach (var order in GetOrdersResponse.orderListResponseModel)
                {
                    if (successfulEnergyOrder.successfulOrderId == order.id)
                    {
                        Assert.IsTrue(successfulEnergyOrder.fuelDescription.ToLower() == order.fuel.ToLower());
                        Assert.IsTrue(successfulEnergyOrder.quanitityToBuy == order.quantity);
                    }
                }
            }
        }

        [TestMethod]
        public async Task Get_All_Successful_Orders_Preceding_Todays_Date()
        {
            //ACT
            var GetOrdersResponse = await _apiHandler.GetOrdersAsync();

            //ASSERT
            Assert.IsNotNull(GetOrdersResponse);

            Assert.AreEqual(200, GetOrdersResponse.statusCode);

            Assert.AreEqual("ok", GetOrdersResponse.statusDescription.ToLower());
  
            Assert.IsNotNull(CompareDateTimeStamps(GetOrdersResponse));
        }

        private List<DateTime> CompareDateTimeStamps(GetOrderListResponseModel GetOrdersResponse)
        {
            List<DateTime> orderDateTimesPrecedingToday = new List<DateTime>();

            foreach (var order in GetOrdersResponse.orderListResponseModel)
            {
                var time = Convert.ToDateTime(order.time);

                if (DateTime.Now > time)
                {
                    orderDateTimesPrecedingToday.Add(Convert.ToDateTime(order.time));
                }
            }

            return orderDateTimesPrecedingToday;
        }

        private async Task<List<EnergyOrderSuccessModel>> CreateEnergyOrders(List<EnergyOrderModel> energyOrdersToCreate)
        {
            List<EnergyOrderSuccessModel> energyOrderSuccessModels = new List<EnergyOrderSuccessModel>();

            ParallelOptions parallelOptions = new ParallelOptions()
            {
                MaxDegreeOfParallelism = energyOrdersToCreate.Count
            };

            await Parallel.ForEachAsync(energyOrdersToCreate, parallelOptions, async (energyOrderToCreate, token) =>
            {
                var energyPurchaseResponse = await _apiHandler.PutBuyEnergyQuantityAsync(energyOrderToCreate.energyId, energyOrderToCreate.quantityToBuy);

                if (energyPurchaseResponse.statusCode == 200)
                {
                    var fuelDescriptionRetrieved = FuelDescriptionHelper.FindFuelDescriptionFromId(energyOrderToCreate.energyId, await _apiHandler.GetEnergyAsync()).ToLower();

                    if (fuelDescriptionRetrieved != String.Empty)
                    {
                        var orderId = OrderIdHelper.FindOrderId(energyPurchaseResponse.buyEnergyQuantityResponseModel.message);

                        energyOrderSuccessModels.Add(
                            new EnergyOrderSuccessModel
                            {
                                successfulOrderId = orderId,
                                fuelDescription = fuelDescriptionRetrieved,
                                quanitityToBuy = energyOrderToCreate.quantityToBuy
                            });
                    }
                }
            });

            return energyOrderSuccessModels;
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            _apiHandler.Dispose();
        }
    }
}
