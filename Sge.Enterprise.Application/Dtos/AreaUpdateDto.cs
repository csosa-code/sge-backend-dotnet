namespace Sge.Enterprise.Application.Dtos;

public class AreaUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int StatusId { get; set; }
}

