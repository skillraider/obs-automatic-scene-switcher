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
}