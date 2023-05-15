using KP.db.dbClasses;
using KP.dbClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KP.db.context
{
    public class DbAppContext : DbContext
    {
        public DbSet<MiniItemInfo> miniItemInfos { get; set; } = null!;
        public DbSet<BigItemInfo> bigItemInfoInfos { get; set; } = null!;
        public DbSet<FramesFromMovie> framesFromMovies { get; set; } = null!;
        public DbSet<Review> reviews { get; set; } = null!;
        public DbSet<UserProfile> userProfiles { get; set; } = null!;
        public DbSet<Likes> userLikes { get; set; } = null!;
        public DbSet<WatchLater> userWatchLater { get; set; }=null!;
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string connectionString = "Server=GOKING;Database=COURSE_PROJECT;Trusted_Connection=True;TrustServerCertificate=True;";
            builder.UseSqlServer(connectionString);
        }
        public DbAppContext()
        {
            /*Database.EnsureDeleted();
            Database.EnsureCreated();*/
        }


    }
}
