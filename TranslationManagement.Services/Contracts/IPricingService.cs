namespace TranslationManagement.Services.Contracts
{
    public interface IPricingService
    {
        double CalculatePrice(int contentLength);
    }
}