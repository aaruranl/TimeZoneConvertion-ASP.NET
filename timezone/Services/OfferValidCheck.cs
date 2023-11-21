namespace timezone.Services
{
    public class OfferValidCheck
    {
        public string OfferMessage(DateTime startTime, DateTime endTime)
        {

            if (startTime <= DateTime.UtcNow && endTime >= DateTime.UtcNow)
            {
                return "You can get offer";
            }
            else if (endTime <= DateTime.UtcNow)
            {
                return "Offer ended";
            }
            else
            {
                return "Please wait for the offer";
            }
        }
    }
}
