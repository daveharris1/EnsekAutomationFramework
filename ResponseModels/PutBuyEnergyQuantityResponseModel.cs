using EnsekAutomationFramework.ApiHandler.ResponseModels;

namespace EnsekAutomationFramework.ResponseModels
{
    public class PutBuyEnergyQuantityResponseModel : BaseResponseModel
    {
        public BuyEnergyQuantityResponseModel? buyEnergyQuantityResponseModel { get; set; }
    }
}