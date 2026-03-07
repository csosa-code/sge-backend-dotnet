using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Domain.Entities;

namespace Sge.Enterprise.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(User user);
}

