using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Response.User;
using Cloud.Service.Infrastructure;
using Cloud.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Cloud.Service.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<UserService> _logger;
    private readonly FileService _file;

    public UserService(IUserRepository repository, IMapper mapper, ILogger<UserService> logger, FileService file)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _file = file;
    }

    public async Task<BaseUserResponse> Get(Guid id)
    {
        var user = await _repository.Get(id);

        if (user == null)
        {
            _logger.LogError($"[User Service || Get]: Пользователь не найден по ID: {id}");
            throw new Exception($"[User Service || Get]: Пользователь не найден по ID: {id}");
        }

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByName(string name)
    {
        var user = await _repository.GetByName(name);

        if (user == null)
        {
            _logger.LogError($"[User Service || Get by name]: Пользователь не найден по Login: {name}");
            throw new Exception($"[User Service || Get by name]: Пользователь не найден по Login: {name}");
        }

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByEmail(string email)
    {
        var user = await _repository.GetByEmail(email);

        if (user == null)
        {
            _logger.LogError($"[User Service || Get by email]: Пользователь не найден по Email: {email}");
            throw new Exception($"[User Service || Get by email]: Пользователь не найден по Email: {email}");
        }

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByPhone(string phone)
    {
        var user = await _repository.GetByPhone(phone);

        if (user == null)
        {
            _logger.LogError($"[User Service || Get by phone]: Пользователь не найден по Phone: {phone}");
            throw new Exception($"[User Service || Get by phone]: Пользователь не найден по Phone: {phone}");
        }

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<ICollection<BaseUserResponse>> SelectAll()
    {
        var users = await _repository.SelectAll();
        return _mapper.Map<ICollection<BaseUserResponse>>(users);
    }

    public async Task<ICollection<BaseUserResponse>> Pagination(uint number, uint size)
    {
        var users = await _repository.Pagination(number, size);
        return _mapper.Map<ICollection<BaseUserResponse>>(users);
    }

    public async Task<BaseUserResponse> Create(CreateUserRequest request)
    {
        var existingUserByEmail = await _repository.GetByEmail(request.Email);
        if (existingUserByEmail != null)
        {
            _logger.LogError($"[User Service || Create]: Пользователь c почтой ' {request.Email}' уже существует");
            throw new InvalidOperationException($"Пользователь c почтой '{request.Email}' уже существует");
        }

        var existingUserByLogin = await _repository.GetByName(request.Login);
        if (existingUserByLogin != null)
        {
            _logger.LogError($"[User Service || Create]: Пользователь c логином '{request.Login}' уже существует");
            throw new InvalidOperationException($"Пользователь c логином '{request.Login}' уже существует");
        }

        var user = _mapper.Map<User>(request);

        var salt = Guid.NewGuid();

        user.Salt = salt.ToString();
        user.Password = PasswordHasherService.HashPassword(user.Password, salt.ToString());

        await _repository.Create(user);
        await _repository.SaveAsync();

        Console.WriteLine(user);
        
        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> Update(UpdateUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
        {
            _logger.LogError($"[User service || update]: пользователя с таким ID: {request.Id}, не существует");
            throw new Exception($"[User service || update]: пользователя с таким ID: {request.Id}, не существует");
        }

        user = _mapper.Map<User>(user);

        _repository.Update(user);
        await _repository.SaveAsync();

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> UpdateAvatar(UpdateAvatarUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
        {
            _logger.LogError($"[User service || update avatar]: пользователя с таким ID: {request.Id}, не существует");
            throw new Exception(
                $"[User service || update avatar]: пользователя с таким ID: {request.Id}, не существует");
        }

        user.Avatar = await _file.FileSaver(request.File, "test_path");

        _repository.Update(user);
        await _repository.SaveAsync();

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<bool> Delete(DeleteUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
        {
            _logger.LogError($"[User Service || delete]: Пользователь не найден по ID: {request.Id}");
            throw new Exception($"[User Service || delete]: Пользователь не найден по ID: {request.Id}");
        }

        _repository.Delete(user);
        await _repository.SaveAsync();

        return true;
    }
}