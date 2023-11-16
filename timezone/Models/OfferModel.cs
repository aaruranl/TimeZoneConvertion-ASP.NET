namespace timezone.Models
{
    public class OfferModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TimeZoneType { get; set; }
        public DateTime Time { get; set; }
    }
}
