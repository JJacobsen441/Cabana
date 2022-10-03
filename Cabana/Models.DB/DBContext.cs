using System.Data.Entity;

namespace Cabana.Models.DB
{
    /*
     * not used
     * */
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=umbracoDbDSN")
        {
        }

        public virtual DbSet<MyUser> myuser { get; set; }
        public virtual DbSet<Movie> movie { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyUser>()
                .HasKey(b => b.Id);

            modelBuilder.Entity<Movie>()
                .HasKey(b => b.Id);

            //modelBuilder.Entity<MyUser>()
            //    .HasMany(e => e.movies);

            //modelBuilder.Entity<Movie>()
            //    .HasRequired(e => e.user);
        }
    }
}/**/