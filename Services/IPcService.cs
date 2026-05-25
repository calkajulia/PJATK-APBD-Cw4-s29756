using PJATK_APBD_Cw4_s29756.DTOs.Requests;
using PJATK_APBD_Cw4_s29756.DTOs.Responses;

namespace PJATK_APBD_Cw4_s29756.Services;

public interface IPcService
{
    Task<IEnumerable<PcResponse>> GetAllAsync();
    Task<PcDetailsResponse?> GetByIdAsync(int id);
    Task<PcResponse> CreateAsync(PcRequest request);
    Task<PcResponse?> UpdateAsync(int id, PcRequest request);
    Task<bool> DeleteAsync(int id);
}
