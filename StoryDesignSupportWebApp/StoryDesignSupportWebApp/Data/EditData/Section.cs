using System.ComponentModel.DataAnnotations;

namespace StoryDesignSupportWebApp.Data.EditData {
    public class Section {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
    }
}
