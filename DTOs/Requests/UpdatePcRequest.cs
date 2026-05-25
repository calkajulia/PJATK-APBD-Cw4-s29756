namespace PJATK_APBD_Cw4_s29756.DTOs.Requests;

public class UpdatePcRequest
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}
