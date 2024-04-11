namespace MyBank.Application.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public AdminService(
        IAdminRepository adminRepository,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider
    )
    {
        _adminRepository = adminRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<ServiceResponse<int>> Register(string login, string password, string nickname)
    {
        var hashedPassword = _passwordHasher.GenerateHash(password);

        var admin = new Admin
        {
            Id = 0,
            Login = login,
            HashedPassword = hashedPassword,
            Nickname = nickname
        };

        var adminId = await _adminRepository.Add(admin);

        return new ServiceResponse<int>
        {
            Status = true,
            Message = "Success",
            Data = adminId
        };
    }

    public async Task<ServiceResponse<(int, string)>> Login(string login, string password)
    {
        var isExist = await _adminRepository.IsExistByLogin(login);

        if (!isExist)
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Администратора с данным логином не найдено",
                Data = (-1, string.Empty)
            };

        var admin = await _adminRepository.GetByLogin(login);

        var res = _passwordHasher.VerifyPassword(password, admin.HashedPassword);

        if (res == false)
        {
            return new ServiceResponse<(int, string)>
            {
                Status = false,
                Message = "Неверный логин или пароль",
                Data = (-1, string.Empty)
            };
        }

        var token = _jwtProvider.GenerateToken(admin);

        return new ServiceResponse<(int, string)>
        {
            Status = true,
            Message = "Success",
            Data = (admin.Id, token)
        };
    }

    public async Task<ServiceResponse<Admin>> GetById(int id)
    {
        var admin = await _adminRepository.GetById(id);

        if (admin == null)
        {
            return new ServiceResponse<Admin>
            {
                Status = false,
                Message = $"Admin with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<Admin>
        {
            Status = true,
            Message = "Success",
            Data = admin
        };
    }

    public async Task<ServiceResponse<bool>> UpdateInfo(int id, string nickname)
    {
        var status = await _adminRepository.UpdateInfo(id, nickname);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe admin with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }

    public async Task<ServiceResponse<bool>> Delete(int id)
    {
        var status = await _adminRepository.Delete(id);

        if (status == false)
        {
            return new ServiceResponse<bool>
            {
                Status = false,
                Message = $"Unknown error. Maybe admin with given id ({id}) not found",
                Data = default
            };
        }

        return new ServiceResponse<bool>
        {
            Status = true,
            Message = "Success",
            Data = status
        };
    }
}
