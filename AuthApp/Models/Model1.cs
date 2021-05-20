using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;


namespace AuthApp.Models
{
    public partial class Model1 : DbContext
    {
        public Model1(DbContextOptions<Model1> options)
            : base(options)
        {
            Database.EnsureCreated();
        }


        public virtual DbSet<employee> employee { get; set; }
        public virtual DbSet<flightattendants> flightattendants { get; set; }
        public virtual DbSet<flights> flights { get; set; }
        public virtual DbSet<flights_has_stations> flights_has_stations { get; set; }
        public virtual DbSet<flights_has_traveltime> flights_has_traveltime { get; set; }
        public virtual DbSet<navigators> navigators { get; set; }
        public virtual DbSet<pilots> pilots { get; set; }
        public virtual DbSet<posts> posts { get; set; }
        public virtual DbSet<radiooperators> radiooperators { get; set; }
        public virtual DbSet<stations> stations { get; set; }
        public virtual DbSet<traveltime> traveltime { get; set; }
        public virtual DbSet<flightcrews> flightcrews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<employee>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<employee>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<flightattendants>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<flightattendants>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<flightattendants>()
                .HasOne(e => e.flightcrew)
                .WithOne(e => e.flightattendants)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<flightcrews>()
                .HasOne(e => e.flights)
                .WithOne(e => e.flightcrews)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<flights>()
                .HasMany(e => e.flights_has_stations)
                .WithOne(e => e.flights)
                .HasForeignKey(e => e.Flights_idFlights)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<flights>()
                .HasMany(e => e.traveltimes)
                .WithOne(e => e.flight)
                .HasForeignKey(e => e.Flights_idFlights)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<navigators>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<navigators>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<navigators>()
                .HasOne(e => e.flightcrew)
                .WithOne(e => e.navigators)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<pilots>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<pilots>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<pilots>()
                .HasOne(e => e.flightcrew)
                .WithOne(e => e.pilots)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<posts>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<posts>()
                .HasMany(e => e.employee)
                .WithOne(e => e.posts)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<radiooperators>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<radiooperators>()
                .Property(e => e.Surname)
                .IsUnicode(false);

            modelBuilder.Entity<radiooperators>()
                .HasOne(e => e.flightcrew)
                .WithOne(e => e.radiooperators)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<stations>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<stations>()
                .HasMany(e => e.flights_has_stations)
                .WithOne(e => e.stations)
                .HasForeignKey(e => e.Stations_IdStations)
                .OnDelete(DeleteBehavior.Cascade);

            /*modelBuilder.Entity<traveltime>()
                .HasMany(e => e.flights_has_traveltime)
                .WithOne(e => e.traveltime)
                .HasForeignKey(e => e.TravelTime_idTravelTime)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<traveltime>()
                .HasOne(e => e.flight)
                .WithMany(e => e.traveltimes)
                .OnDelete(DeleteBehavior.Cascade);

            /*modelBuilder.Entity<flights_has_traveltime>()
                .HasOne(e => e.traveltime)
                .WithMany(e => e.flights_has_traveltime)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<flights_has_stations>()
                .HasOne(e => e.flights)
                .WithMany(e => e.flights_has_stations)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<flights_has_stations>()
                .HasOne(e => e.stations)
                .WithMany(e => e.flights_has_stations)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
