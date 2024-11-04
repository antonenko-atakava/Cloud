using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.User;
using Cloud.Domain.Http.Response.User;
using Cloud.Service.Infrastructure;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly ImageService _image;
    private readonly IDirectoryRepository _directory;

    public UserService(IUserRepository repository, IMapper mapper, ImageService image, IDirectoryRepository directory)
    {
        _repository = repository;
        _mapper = mapper;
        _image = image;
        _directory = directory;
    }

    public async Task<BaseUserResponse> Get(Guid id)
    {
        var user = await _repository.Get(id);

        if (user == null)
            throw new Exception($"[User Service || Get]: Пользователь не найден по ID: {id}");

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByName(string name)
    {
        var user = await _repository.GetByName(name);

        if (user == null)
            throw new Exception($"[User Service || Get by name]: Пользователь не найден по Login: {name}");

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByEmail(string email)
    {
        var user = await _repository.GetByEmail(email);

        if (user == null)
            throw new Exception($"[User Service || Get by email]: Пользователь не найден по Email: {email}");

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> GetByPhone(string phone)
    {
        var user = await _repository.GetByPhone(phone);

        if (user == null)
            throw new Exception($"[User Service || Get by phone]: Пользователь не найден по Phone: {phone}");

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
            throw new Exception($"[User Service || Create]: Пользователь c почтой '{request.Email}' уже существует");

        var existingUserByLogin = await _repository.GetByName(request.Login);

        if (existingUserByLogin != null)
            throw new Exception($"[User Service || Create]: Пользователь c логином '{request.Login}' уже существует");

        var user = _mapper.Map<User>(request);

        var salt = Guid.NewGuid();

        user.Salt = salt.ToString();
        user.Password = request.Password;

        user.Password = PasswordHasherService.HashPassword(user.Password, user.Salt);

        await _repository.Create(user);
        await _repository.SaveAsync();

        var newUser = await GetByName(user.Login);
        
        Directory.CreateDirectory(@"C:\Cloud\Users" + "\\" + $"{newUser.Login}_base_directory");
        
        var createDirectory = new CustomDirectory()
        {   
            Icon = "base_image_folder.jpg",
            Name = $"{newUser.Login}_base_directory",
            Path = @$"C:\Cloud\Users\{newUser.Login}_base_directory",
            AtCreate = DateTime.UtcNow,
            AtUpdate = DateTime.UtcNow,
            PathParentDirectory = @$"C:\Cloud\Users",
            OwnerId = newUser.Id,
        };

        await _directory.Create(createDirectory);
        await _directory.SaveAsync();
        
        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<Guid> Login(string login, string password)
    {
        var user = await _repository.GetByName(login);

        if (user == null)
            throw new Exception($"[User Service || Login]: Пользователя c логином '{login}' не существует");

        var passwordHash = PasswordHasherService.HashPassword(password, user.Salt);
        var verifyUser = await _repository.Login(login, passwordHash);

        if (verifyUser == null)
            throw new Exception($"[User Service || Login]: не верный логин или пароль");

        return user.Id;
    }

    public async Task<BaseUserResponse> Update(UpdateUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
            throw new Exception($"[User service || update]: пользователя с таким ID: {request.Id}, не существует");

        if (request.Login != null)
        {
            var userByName = await _repository.GetByName(request.Login);

            if (userByName != null)
                throw new Exception("[User service || update]: такой логин уже занят");
        }

        user = _mapper.Map<User>(user);

        user.Modified = DateTime.UtcNow;

        _repository.Update(user);
        await _repository.SaveAsync();

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<BaseUserResponse> UpdateAvatar(UpdateAvatarUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
            throw new Exception(
                $"[User service || update avatar]: пользователя с таким ID: {request.Id}, не существует");

        user.Avatar = await _image.FileSaver(request.File, @"C:\Cloud\Avatar");

        _repository.Update(user);
        await _repository.SaveAsync();

        return _mapper.Map<BaseUserResponse>(user);
    }

    public async Task<bool> Delete(DeleteUserRequest request)
    {
        var user = await _repository.Get(request.Id);

        if (user == null)
            throw new Exception($"[User Service || delete]: Пользователь не найден по ID: {request.Id}");

        _repository.Delete(user);
        await _repository.SaveAsync();

        return true;
    }
}