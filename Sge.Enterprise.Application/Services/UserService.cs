using Sge.Enterprise.Application.Interfaces;
using Sge.Enterprise.Domain.Entities;
using Sge.Enterprise.Domain.Interfaces;
using AutoMapper;
using Sge.Enterprise.Application.Dtos;
using Sge.Enterprise.Application.Exceptions;
using Sge.Enterprise.Application.Security;

namespace Sge.Enterprise.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;
    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto data)
    {
        var user = await _unitOfWork.Users.GetByUsernameAsync(data.Username);

        if (user == null)
            throw new NotFoundException("Usuario no encontrado");

        if (user.StatusId != 1)
            throw new BadRequestException("Usuario inactivo");

        var password = data.Password.Trim();    

        if (!PasswordExtensions.ValidatePassword(user.PasswordHash, user.PasswordSalt, password))
            throw new BadRequestException("Contraseña incorrecta");

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            Token = token,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public async Task<RegisterDto> RegisterAsync(RegisterDto data)
    {
        data.Email = data.Email.ToLower().Trim();
        var userExists = await _unitOfWork.Users.GetByUsernameAsync(data.Email);

        if (userExists != null)
            throw new BadRequestException("El correo electrónico ya está en uso");


        var passwordResult = PasswordExtensions.HashPassword(data.Password);    

        var user = _mapper.Map<User>(data);
        user.Username = user.Email;
        user.PasswordHash = passwordResult.PasswordHash;
        user.PasswordSalt = passwordResult.PasswordSalt;

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<RegisterDto>(user);
    }
}

