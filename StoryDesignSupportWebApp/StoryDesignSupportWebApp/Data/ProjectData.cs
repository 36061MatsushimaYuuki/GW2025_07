using StoryDesignSupportWebApp.Data.EditData;
using System.ComponentModel.DataAnnotations;

namespace StoryDesignSupportWebApp.Data {
    public class ProjectData {
        public ScenarioData Scenario { get; set; } = new();
    }
}
