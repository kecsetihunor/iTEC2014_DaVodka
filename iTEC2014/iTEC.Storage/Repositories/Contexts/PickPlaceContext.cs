using iTEC.Storage.Entities;
using System.Data.Entity;

namespace iTEC.Storage.Repositories.Contexts
{
    public class PickPlaceContext : DbContext
    {
        public DbSet<Account> Users { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<PlaceOwnership> Ownerships { get; set; }

        public PickPlaceContext()
            : base("DefaultConnection")
        {
        }
    }
}