using System.ComponentModel.DataAnnotations;

namespace StoryDesignSupportWebApp.Data.EditData {
    public class Chapter {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Overview { get; set; } = "";
        public Section[]? Sections { get; set; }
    }
}
