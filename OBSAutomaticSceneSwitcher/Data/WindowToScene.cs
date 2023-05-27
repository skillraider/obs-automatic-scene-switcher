using System.ComponentModel.DataAnnotations;

namespace OBSAutomaticSceneSwitcher;

public class WindowToScene
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string WindowSearch { get; set; } = "";

    [Required]
    public string SceneName { get; set; } = "";

    [Required]
    public MapType MapType { get; set; }
}

public enum MapType
{
    Source,
    Scene
}