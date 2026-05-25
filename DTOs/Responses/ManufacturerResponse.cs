namespace PJATK_APBD_Cw4_s29756.DTOs.Responses;

public class ManufacturerResponse
{
    public int Id { get; set; }
    public string Abbreviation { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public DateOnly FoundationDate { get; set; }
}
