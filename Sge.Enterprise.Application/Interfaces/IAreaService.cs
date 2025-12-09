using Sge.Enterprise.Application.Dtos;

namespace Sge.Enterprise.Application.Interfaces;

public interface IAreaService
{
    Task<IReadOnlyList<AreaDto>> GetAllAsync();
    Task<AreaDto> GetByIdAsync(int areaId);
    Task<AreaDto> CreateAsync(AreaAddDto data);
    Task<AreaDto> UpdateAsync(int areaId, AreaUpdateDto data);
}

