using Microsoft.EntityFrameworkCore;
using PJATK_APBD_Cw4_s29756.Data;
using PJATK_APBD_Cw4_s29756.DTOs.Requests;
using PJATK_APBD_Cw4_s29756.DTOs.Responses;
using PJATK_APBD_Cw4_s29756.Mappers;

namespace PJATK_APBD_Cw4_s29756.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _context;

    public PcService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PcResponse>> GetAllAsync()
    {
        return await _context.Pcs
            .Select(pc => pc.ToResponse())
            .ToListAsync();
    }

    public async Task<PcDetailsResponse?> GetByIdAsync(int id)
    {
        var pc = await _context.Pcs
            .Include(pc => pc.PcComponents)
                .ThenInclude(pcComponent => pcComponent.Component)
                    .ThenInclude(component => component.Manufacturer)
            .Include(pc => pc.PcComponents)
                .ThenInclude(pcComponent => pcComponent.Component)
                    .ThenInclude(component => component.Type)
            .FirstOrDefaultAsync(pc => pc.Id == id);

        return pc?.ToDetailsResponse();
    }

    public async Task<PcResponse> CreateAsync(PcRequest request)
    {
        var pc = request.ToEntity();

        _context.Pcs.Add(pc);
        await _context.SaveChangesAsync();

        return pc.ToResponse();
    }

    public async Task<PcResponse?> UpdateAsync(int id, PcRequest request)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return null;

        pc.Name = request.Name;
        pc.Weight = request.Weight;
        pc.Warranty = request.Warranty;
        pc.CreatedAt = request.CreatedAt;
        pc.Stock = request.Stock;

        await _context.SaveChangesAsync();

        return pc.ToResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _context.Pcs.FindAsync(id);

        if (pc == null)
            return false;

        _context.Pcs.Remove(pc);
        await _context.SaveChangesAsync();

        return true;
    }
}
