namespace PJATK_APBD_Cw4_s29756.DTOs.Responses;

public class ComponentResponse
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ManufacturerResponse Manufacturer { get; set; } = null!;
    public ComponentTypeResponse Type { get; set; } = null!;
}
