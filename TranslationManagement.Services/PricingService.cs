using TranslationManagement.Services.Contracts;

namespace TranslationManagement.Services
{
    public class PricingService : IPricingService
    {
        private const double PricePerCharacter = 0.01; //can be changed to point to app config

        public double CalculatePrice(int contentLength)
        {
            return contentLength * PricePerCharacter;
        }
    }
}
