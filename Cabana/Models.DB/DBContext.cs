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
            //modelBuilder.Entity<MyUser>()
            //    .HasMany(b => b.Movies);

            //modelBuilder.Entity<Movie>()
            //    .HasRequired(e => e.MyUser);

            modelBuilder.Entity<MyUser>()
                .HasMany(b => b.Movies)
                .WithRequired(b => b.MyUser)
                .HasForeignKey(b => b.MyUserID)
                ;

            //modelBuilder.Entity<Movie>()
            //    .HasRequired(e => e.MyUser)
            //    .WithMany(e=>e.Movies)
            //    .HasForeignKey(e=>e.MovieID)
            //    ;
        }
    }
}/**/