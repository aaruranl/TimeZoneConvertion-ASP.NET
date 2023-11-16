using Microsoft.EntityFrameworkCore;
using timezone.Models;

namespace timezone.DataBase
{
    public class TimeDBContext : DbContext
    {
        public TimeDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<OfferModel> OfferModels { get; set; }
    }
}
