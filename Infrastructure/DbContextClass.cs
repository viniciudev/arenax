using Core;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> contextOptions) : base(contextOptions)
        { }

        public DbSet<User> User { get; set; }
        public DbSet<SportsCourt> SportsCourt { get; set; }
        public DbSet<SportsCourtOperation> SportsCourtOperations { get; set; }
        public DbSet<CourtEvaluations> CourtEvaluations { get; set; }
        public DbSet<SportsCategory> SportsCategory { get; set; }
        public DbSet<SportsCourtAppointments> SportsCourtAppointments { get; set; }
        public DbSet<SportsCenter> SportsCenter { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<SportsCenterUsers> SportsCenterUsers { get; set; }
        public DbSet<SportsCourtCategory> SportsCourtCategory { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<SportsCourtImage> SportsCourtImage { get; internal set; }
        public DbSet<SportsRegistration> SportsRegistration { get; internal set; }
        public DbSet<Notifications> Notifications { get; internal set; }
        public DbSet<ClientEvaluation> ClientEvaluation { get; internal set; }
        public DbSet<Otp> Otp { get; internal set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // No OnModelCreating
            modelBuilder.Entity<Otp>(entity =>
            {
                entity.ToTable("Otp");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Id).ValueGeneratedOnAdd();
           
            });

            modelBuilder.Entity<ClientEvaluation>(entity =>
            {
                entity.ToTable("ClientEvaluation");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ClientEvaluation>()
            .HasOne(x => x.Client)
            .WithMany(x => x.ClientEvaluations)
            .HasForeignKey(x => x.IdClient);

            modelBuilder.Entity<ClientEvaluation>()
           .HasOne(x => x.SportsCourtAppointments)
           .WithMany(x => x.ClientEvaluations)
           .HasForeignKey(x => x.IdAppointments);

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.ToTable("Notifications");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Notifications>()
            .HasOne(x => x.Client)
            .WithMany(x => x.Notifications)
            .HasForeignKey(x => x.IdClient);

            modelBuilder.Entity<SportsRegistration>(entity =>
            {
                entity.ToTable("SportsRegistration");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SportsRegistration>()
            .HasOne(x => x.SportsCourtAppointments)
            .WithMany(x => x.SportsRegistrations)
            .HasForeignKey(x => x.IdSportsCourtAppointments);
                    modelBuilder.Entity<SportsRegistration>()
            .HasOne(x => x.Client)
            .WithMany(x => x.SportsRegistrations)
            .HasForeignKey(x => x.IdClient);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SportsCourt>(entity =>
            {
                entity.ToTable("SportsCourt");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SportsCourt>()
               .HasOne(x => x.SportsCenter)
               .WithMany(x => x.SportsCourts)
               .HasForeignKey(x => x.IdSportsCenter);


        

            modelBuilder.Entity<SportsCategory>(entity =>
            {
                entity.ToTable("SportsCategory");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SportsCourtOperation>(entity =>
            {
                entity.ToTable("SportsCourtOperation");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SportsCourtOperation>()
      .HasOne(x => x.SportsCourt)
      .WithMany(x => x.SportsCourtOperations)
      .HasForeignKey(x => x.IdSportsCourt);

            modelBuilder.Entity<CourtEvaluations>(entity =>
            {
                entity.ToTable("CourtEvaluations");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CourtEvaluations>()
   .HasOne(x => x.SportsCourt)
   .WithMany(x => x.CourtEvaluations)
   .HasForeignKey(x => x.IdSportsCourt);

            modelBuilder.Entity<SportsCourtOperation>()
      .HasOne(x => x.SportsCourt)
      .WithMany(x => x.SportsCourtOperations)
      .HasForeignKey(x => x.IdSportsCourt);

            modelBuilder.Entity<SportsCourtAppointments>(entity =>
            {
                entity.ToTable("SportsCourtAppointments");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SportsCourtAppointments>()
   .HasOne(x => x.SportsCourt)
   .WithMany(x => x.SportsCourtAppointments)
   .HasForeignKey(x => x.IdSportsCourt);

            modelBuilder.Entity<SportsCourtAppointments>()
              .HasOne(x => x.Client)
              .WithMany(x => x.SportsCourtAppointments)
              .HasForeignKey(x => x.IdClient);
            
            modelBuilder.Entity<SportsCourtAppointments>()
            .HasOne(x => x.SportsCategory)
            .WithMany(x => x.SportsCourtAppointments)
            .HasForeignKey(x => x.IdCategory);

            modelBuilder.Entity<SportsCenter>(entity =>
            {
                entity.ToTable("SportsCenter");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SportsCenter>()
            .HasOne(s => s.Address)
            .WithOne(a => a.SportsCenter)
            .HasForeignKey<Address>(a => a.SportsCenterId);

            modelBuilder.Entity<SportsCenter>()
                .HasOne(s => s.OpeningHours)
                .WithOne(o => o.SportsCenter)
                .HasForeignKey<OpeningHours>(o => o.SportsCenterId);

            modelBuilder.Entity<SportsCenterUsers>(entity =>
            {
                entity.ToTable("SportsCenterUsers");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SportsCenterUsers>()
           .HasOne(scu => scu.User)
           .WithMany(u => u.SportsCenterUsers) // Assuming User has a collection
           .HasForeignKey(scu => scu.IdUser);

            modelBuilder.Entity<SportsCenterUsers>()
            .HasOne(scu => scu.SportsCenter)
            .WithMany(sc => sc.SportsCenterUsers) // Assuming SportsCenter has a collection
            .HasForeignKey(scu => scu.IdSportsCenter);

            modelBuilder.Entity<SportsCourtCategory>(entity =>
            {
                entity.ToTable("SportsCourtCategory");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SportsCourtCategory>()
           .HasOne(scu => scu.SportsCategory)
           .WithMany(u => u.SportsCourtCategories) // Assuming User has a collection
           .HasForeignKey(scu => scu.SportsCategoryId);

            modelBuilder.Entity<SportsCourtCategory>()
            .HasOne(scu => scu.SportsCourt)
            .WithMany(sc => sc.SportsCourtCategories) // Assuming SportsCenter has a collection
            .HasForeignKey(scu => scu.SportsCourtId);

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            // Configuração do relacionamento 1:1
            modelBuilder.Entity<Client>()
                .HasOne(u => u.User)
                .WithOne(p => p.Client)
                .HasForeignKey<User>(p => p.IdClient);

            modelBuilder.Entity<SportsCourtImage>(entity =>
            {
                entity.ToTable("SportsCourtImage");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<SportsCourtImage>()
           .HasOne(scu => scu.SportsCourt)
           .WithMany(u => u.sportsCourtImages) // Assuming User has a collection
           .HasForeignKey(scu => scu.SportsCourtId);

            base.OnModelCreating(modelBuilder);
        }


        public override int SaveChanges()
        {
            var entries = ChangeTracker
              .Entries()
              .Where(e => e.Entity is BaseModel && (
                      e.State == EntityState.Added
                      || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseModel)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseModel)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
            return base.SaveChanges();
        }
    }
}
