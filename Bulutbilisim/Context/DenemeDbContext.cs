
using Microsoft.EntityFrameworkCore;

namespace Bulutbilisim.Context
{
    public class DenemeDbContext : DbContext
    {
        public DenemeDbContext()
        {
        }

        public DenemeDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Deneme> Denemes { get; set; }
        DbSet<Tabloiki> Tabloiki { get; set; }
    }
}
