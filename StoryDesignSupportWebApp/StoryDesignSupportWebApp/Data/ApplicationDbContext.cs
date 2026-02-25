using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Reflection.Emit;
using System.Text.Json;
using System.Transactions;

namespace StoryDesignSupportWebApp.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes()) {
                foreach (var property in entity.GetProperties()) {
                    if (property.ClrType == typeof(string)) {
                        if (property.GetMaxLength() == null) {
                            property.SetMaxLength(4000);
                        }
                    }
                }
            }

            var jsonOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            var dataConverter = new ValueConverter<ProjectData, string>(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => string.IsNullOrWhiteSpace(v)
                        ? new ProjectData()
                        : JsonSerializer.Deserialize<ProjectData>(v, jsonOptions)!);

            var dataComparer = new ValueComparer<ProjectData>(
                (a, b) => JsonSerializer.Serialize(a, jsonOptions) == JsonSerializer.Serialize(b, jsonOptions),
                v => JsonSerializer.Serialize(v, jsonOptions).GetHashCode(),
                v => JsonSerializer.Deserialize<ProjectData>(
                         JsonSerializer.Serialize(v, jsonOptions), jsonOptions)!);

            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectDataObject)
                .HasConversion(dataConverter)
                .HasColumnType("TEXT")
                .HasAnnotation("Relational:ColumnOrder", 0)
                .Metadata.SetValueComparer(dataComparer);
        }
    }
}
