using Sge.Enterprise.Application.Interfaces;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using AutoMapper;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Exceptions;

namespace Sge.Enterprise.Application.Services;

public class AreaService : IAreaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AreaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<AreaDto>> GetAllAsync()
    {
        var areas = await _unitOfWork.Areas.GetAllAsync();
        return _mapper.Map<IReadOnlyList<AreaDto>>(areas);
    }

    public async Task<AreaDto> GetByIdAsync(int areaId)
    {
        var area = await GetAreaAsync(areaId);
        return _mapper.Map<AreaDto>(area);
    }

    public async Task<AreaDto> CreateAsync(AreaAddDto data)
    {
        await ValidateUniqueNameAsync(data.Name);

        var area = _mapper.Map<Area>(data);

        await _unitOfWork.Areas.AddAsync(area);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<AreaDto>(area);
    }

    public async Task<AreaDto> UpdateAsync(int areaId, AreaUpdateDto data)
    {
        try
        {
            var curArea = await GetAreaAsync(areaId);

            _mapper.Map(data, curArea);

            await _unitOfWork.Areas.UpdateAsync(curArea);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AreaDto>(curArea);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private async Task<Area> GetAreaAsync(int areaId)
    {
        return await _unitOfWork.Areas.GetByIdAsync(areaId) ?? throw new NotFoundException("Área no encontrada");
    }

    private async Task ValidateUniqueNameAsync(string name)
    {
        var exists = await _unitOfWork.Areas.ExistsByNameAsync(name);
        if (exists)
            throw new BadRequestException("El nombre del área ya está en uso.");
    }
}

