using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Web.Infru.Enitites;

namespace Web.Infru.DBContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IApplicationDbContext
    {
        
          
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public ApplicationDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    b => b.MigrationsAssembly(_migrationAssemblyName)
                );
            }

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().ToTable("Topics");

            modelBuilder.Entity<CourseRegistration>().HasKey(c => new { c.CourseId, c.StudentId });

            modelBuilder.Entity<Course>()
                .HasMany(n => n.Topics)
                .WithOne(a => a.Course)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(a => a.Course)
                .WithMany(n => n.CourseStudents)
                .HasForeignKey(x => x.CourseId);

            modelBuilder.Entity<CourseRegistration>()
                .HasOne(a => a.Student)
                .WithMany(n => n.StudentCourses)
                .HasForeignKey(x => x.StudentId);

            base.OnModelCreating(modelBuilder);
        }
        public  DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }



    }

}