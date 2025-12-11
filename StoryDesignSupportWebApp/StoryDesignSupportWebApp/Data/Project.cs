namespace StoryDesignSupportWebApp.Data {
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Project {
        public const int MaxGenreSize = 5;

        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = "";

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = "";

        [MaxLength(50)]
        public string[]? Genre { get; set; }

        [MaxLength(400)]
        public string Overview { get; set; } = "";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Project() {
            //初期化
            Genre = new string[MaxGenreSize];
        }
    }
}
