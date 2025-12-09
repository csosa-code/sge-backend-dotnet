namespace Sge.Enterprise.Application.Dtos;

public class AreaDto
{
    public int AreaId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int StatusId { get; set; }
}

