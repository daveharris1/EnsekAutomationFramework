using EnsekAutomationFramework.ApiHandler.RequestModels;
using EnsekAutomationFramework.ApiHandler.ResponseModels;
using EnsekAutomationFramework.ApiHelper.ResponseModels;
using Newtonsoft.Json;
using System.Text;

namespace EnsekAutomationFramework.ApiHandler
{
    public class RestApiHandler : IDisposable
    {
        private HttpClient _httpClient;

        public RestApiHandler(string baseUrl)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        // POST
        public async Task<PostLoginResponseModel> PostLoginAsync(PostLoginRequestModel postLoginRequestModel)
        {
            var response = await _httpClient.PostAsync("/ENSEK/login", new StringContent(JsonConvert.SerializeObject(postLoginRequestModel), Encoding.UTF8, "application/json"));

            var postLoginResponseModel = new PostLoginResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                postLoginResponseModel.loginResponseModel = JsonConvert.DeserializeObject<LoginResponseModel>(content);
            }

            return postLoginResponseModel;
        }

        public async Task<PostResetDataResponseModel> PostResetDataAsync(string bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Add("Authorization", bearerToken);

            var response = await _httpClient.PostAsync("/ENSEK/reset", null);

            var postResetDataResponseModel = new PostResetDataResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                postResetDataResponseModel.resetDataResponseModel = JsonConvert.DeserializeObject<ResetDataResponseModel>(content);
            }

            return postResetDataResponseModel;
        }

        //PUT
        public async Task<PutBuyEnergyQuantityResponseModel> PutBuyEnergyQuantityAsync(int energyId, int quantityToBuy)
        {
            var response = await _httpClient.PutAsync($"/ENSEK/buy/{energyId}/{quantityToBuy}", null);

            var putBuyEnergyQuantityResponseModel = new PutBuyEnergyQuantityResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                putBuyEnergyQuantityResponseModel.buyEnergyQuantityResponseModel = JsonConvert.DeserializeObject<BuyEnergyQuantityResponseModel>(content);
            }

            return putBuyEnergyQuantityResponseModel;
        }

        // GET
        public async Task<GetOrderListResponseModel> GetOrderByIdAsync(string orderId)
        {
            var response = await _httpClient.GetAsync($"/ENSEK/orders/{orderId}");

            var getOrderListResponseModel = new GetOrderListResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    getOrderListResponseModel.orderListResponseModel = JsonConvert.DeserializeObject<List<OrderListResponseModel>>(content);
                }
            }

            return getOrderListResponseModel;
        }

        public async Task<GetOrderListResponseModel> GetOrdersAsync()
        {
            var response = await _httpClient.GetAsync($"/ENSEK/orders");

            var getOrderResponseModel = new GetOrderListResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                getOrderResponseModel.orderListResponseModel = JsonConvert.DeserializeObject<List<OrderListResponseModel>>(content);
            }

            return getOrderResponseModel;
        }

        public async Task<GetEnergyResponseModel> GetEnergyAsync()
        {
            var response = await _httpClient.GetAsync($"/ENSEK/energy");

            var getEnergyResponseModel = new GetEnergyResponseModel
            {
                statusCode = Convert.ToInt32(response.StatusCode),
                statusDescription = response.ReasonPhrase
            };

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                getEnergyResponseModel.energyResponseModel = JsonConvert.DeserializeObject<EnergyResponseModel>(content);
            }

            return getEnergyResponseModel;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}