using timezone.Models;

namespace timezone.Api.Models
{
    public class OfferResponseModel : OfferModel
    {
        public string validOfferMsg { get; set; }
        public DateTime currentTime { get; set; }
    }
}
