using EnsekAutomationFramework.ApiHandler.EnergyModels;

namespace EnsekAutomationFramework.ApiHelper.ResponseModels
{
    public class EnergyResponseModel
    {
        public ElectricModel? electric { get; set; }
        public GasModel? gas { get; set; }
        public NuclearModel? nuclear { get; set; }
        public OilModel? oil { get; set; }
    }
}
