using System.ComponentModel.DataAnnotations;

namespace OBSAutomaticSceneSwitcher;

public class ConnectionSettings
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string IpPort { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}