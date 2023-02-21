namespace EnsekAutomationFramework.TestHelpers
{
    public static class OrderIdHelper
    {
        public static string FindOrderId(string response)
        {
            //Remove the . from the end of the string
            var reducedString = response.Remove(response.Length - 1);

            string guidFound = string.Empty;

            Guid guid = Guid.Empty;

            foreach (var stringValue in reducedString.Split())
            {
                if (Guid.TryParse(stringValue, out guid))
                {
                    break;
                }
            }

            if (guid != Guid.Empty)
            {
                return guid.ToString();
            }

            return string.Empty;
        }
    }
}
