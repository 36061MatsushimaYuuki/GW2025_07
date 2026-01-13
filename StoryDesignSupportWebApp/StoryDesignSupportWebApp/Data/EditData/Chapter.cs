namespace StoryDesignSupportWebApp.Data.EditData {
    public class Chapter {
        public string Title { get; set; } = "";
        public string Overview { get; set; } = "";
        public Section[]? Sections { get; set; }
    }
}
