using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
using System.Transactions;

namespace StoryDesignSupportWebApp.Data {
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options) {
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            // JSONシリアライズ設定（必要ならオプションを調整）
            var jsonOptions = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // null の扱い、既定値の書き出しなど必要に応じて
                // DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            // ValueConverter: ProjectData <-> string(JSON)
            var dataConverter = new ValueConverter<ProjectData, string>(
                v => JsonSerializer.Serialize(v, jsonOptions),
                v => string.IsNullOrWhiteSpace(v)
                        ? new ProjectData()
                        : JsonSerializer.Deserialize<ProjectData>(v, jsonOptions)!);

            // ValueComparer: 差分検出を JSON同値で判定
            var dataComparer = new ValueComparer<ProjectData>(
                (a, b) => JsonSerializer.Serialize(a, jsonOptions) == JsonSerializer.Serialize(b, jsonOptions),
                v => JsonSerializer.Serialize(v, jsonOptions).GetHashCode(),
                v => JsonSerializer.Deserialize<ProjectData>(
                         JsonSerializer.Serialize(v, jsonOptions), jsonOptions)!);

            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectDataObject)
                .HasConversion(dataConverter)
                .HasColumnType("nvarchar(max)")   // SQL Server の場合
                .HasAnnotation("Relational:ColumnOrder", 0) // 任意。列順を制御したいなら
                .Metadata.SetValueComparer(dataComparer);
        }
    }
}
