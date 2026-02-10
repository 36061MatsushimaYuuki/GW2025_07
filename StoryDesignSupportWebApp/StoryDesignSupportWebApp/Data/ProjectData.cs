using Microsoft.EntityFrameworkCore.Diagnostics;
using StoryDesignSupportWebApp.Data.EditData;
using System.ComponentModel.DataAnnotations;
using System.Xml;

namespace StoryDesignSupportWebApp.Data {
    public class ProjectData {
        public ScenarioData Scenario { get; set; } = new();
        public StructureData Structure { get; set; } = new();
        public CharacterData Character { get; set; } = new();
        public MaterialData Material { get; set; } = new();
    }
}
