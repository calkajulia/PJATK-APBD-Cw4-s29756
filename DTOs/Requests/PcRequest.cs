using System.ComponentModel.DataAnnotations;

namespace PJATK_APBD_Cw4_s29756.DTOs.Requests;

public class PcRequest
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    [Range(0.01, double.MaxValue)]
    public double Weight { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Warranty { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
