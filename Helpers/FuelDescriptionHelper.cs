using ApiHelper.ResponseModels;

namespace EnsekAutomationFramework.Helpers
{
    public static class FuelDescriptionHelper
    {
        public static string FindFuelDescriptionFromId(int energyId, GetEnergyResponseModel getEnergyResponseModel)
        {
            string energyDescription = string.Empty;

            if (energyId == getEnergyResponseModel.energyResponseModel.oil.energy_id)
            {
                energyDescription = "oil";
            }
            else if (energyId == getEnergyResponseModel.energyResponseModel.electric.energy_id)
            {
                energyDescription = "electric";
            }
            else if (energyId == getEnergyResponseModel.energyResponseModel.electric.energy_id)
            {
                energyDescription = "elec";
            }
            else if (energyId == getEnergyResponseModel.energyResponseModel.nuclear.energy_id)
            {
                energyDescription = "nuclear";
            }
            else if (energyId == getEnergyResponseModel.energyResponseModel.gas.energy_id)
            {
                energyDescription = "gas";
            }

            return energyDescription;
        }
    }
}
