namespace EnsekAutomationFramework.ApiHandler.EnergyModels
{
    public class OilModel
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string? unit_type { get; set; }
    }
}
