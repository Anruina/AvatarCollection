using AvatarCollectionLibrary;
using Microsoft.EntityFrameworkCore;

namespace AvatarCollection.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<Collectable> Collectables { get; set; }
        public DbSet<MyCollection> MyCollections { get; set; }
        public DbSet<User> Users { get; set; }

        public DataDbContext(DbContextOptions<DataDbContext> options)
                : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        //models in db zetten
        protected override void OnModelCreating(ModelBuilder builder)
        {

            //fluent api
            builder.Entity<Collectable>()
                .Property(c => c.Releasedate)
                .HasConversion(
                    v => v.ToString(),
                    v => (DateOnly)Enum.Parse(typeof(DateOnly), v));

            //          builder.Entity<Collectable>()
            //            .Property(m => m.Price)
            //            .HasConversion<Decimal>();

            //         builder.Entity<Collectable>()
            //              .Property(w => w.Worth)
            //.HasConversion<Decimal>();

            base.OnModelCreating(builder);
        }
    }
}
