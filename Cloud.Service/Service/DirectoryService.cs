using AutoMapper;
using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Cloud.Domain.Http.Request.Directory;
using Cloud.Domain.Http.Response.Directory;
using Cloud.Service.Infrastructure;
using Cloud.Service.Interface;

namespace Cloud.Service.Service;

public class DirectoryService : IDirectoryService
{
    private readonly IDirectoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ImageService _imageService;

    public DirectoryService(IDirectoryRepository repository, IMapper mapper, ImageService imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<BaseDirectoryResponse> Get(Guid id)
    {
        var directory = await _repository.Get(id);
        return _mapper.Map<BaseDirectoryResponse>(directory);
    }

    public async Task<BaseDirectoryResponse> GetByPath(string path)
    {
        var directory = await _repository.GetByPath(path);
        return _mapper.Map<BaseDirectoryResponse>(directory);
    }

    public async Task<ICollection<BaseDirectoryResponse>> GetAllUserDirectories(Guid userId)
    {
        var directory = await _repository.GetAllUserDirectories(userId);
        return _mapper.Map<ICollection<BaseDirectoryResponse>>(directory);
    }

    public async Task<ICollection<BaseDirectoryResponse>> GetByName(string name, Guid userId)
    {
        var directory = await _repository.GetByName(name, userId);
        return _mapper.Map<ICollection<BaseDirectoryResponse>>(directory);
    }

    public async Task<ICollection<BaseDirectoryResponse>> GetSubDirectories(Guid id)
    {
        var directory = await _repository.GetSubDirectories(id);
        return _mapper.Map<ICollection<BaseDirectoryResponse>>(directory);
    }

    public async Task<BaseDirectoryResponse> Create(CreateDirectoryRequest request)
    {
        var directory = await _repository.GetByPath(request.Path);

        if (directory == null)
            throw new Exception("[Create || Directory Service]: По указанному пути не существует директории");

        FilterDirectoryByName(request.Name, request.Path);
    
        var newDirectory = Directory.CreateDirectory(request.Path + "\\" + request.Name);

        var createDirectory = new CustomDirectory()
        {
            ParentId = directory?.Id,
            Icon = "base_image_folder.jpg",
            Name = request.Name,
            Path = request.Path + '\\' + request.Name,
            AtCreate = newDirectory.CreationTime.ToUniversalTime(),
            AtUpdate = newDirectory.LastWriteTime.ToUniversalTime(),
            PathParentDirectory = request.Path,
            OwnerId = request.UserId,
        };

        await _repository.Create(createDirectory);
        await _repository.SaveAsync();

        return _mapper.Map<BaseDirectoryResponse>(createDirectory);
    }

    public async Task<BaseDirectoryResponse> Rename(UpdateDirectoryRequest request)
    {
        var directory = await _repository.Get(request.Id);

        if (directory == null)
            throw new Exception("[Rename || Directory Service]: Директории с таким id не существует");

        if (!Directory.Exists(directory.PathParentDirectory))
            throw new Exception("[Rename || Directory Service]: Родительской директории не существует");

        var newFullPath = Path.Combine(directory.PathParentDirectory, request.Name);
        
        FilterDirectoryByName(request.Name, directory.PathParentDirectory);

        var directoryInfo = new DirectoryInfo(directory.Path);

        directoryInfo.MoveTo(newFullPath);

        directory.Name = request.Name;
        directory.Path = directory.PathParentDirectory + '\\' + request.Name;
        directory.AtUpdate = DateTime.UtcNow;

        _repository.Update(directory);
        await _repository.SaveAsync();

        await UpdateSubDirectoryPath(directory.Id, directory.Path);

        return _mapper.Map<BaseDirectoryResponse>(directory);
    }

    public async Task<BaseDirectoryResponse> UpdateIcon(UpdateIconDirectoryRequest request)
    {
        var directory = await _repository.Get(request.Id);

        if (directory == null)
            throw new Exception("[Update Icon || Directory Service]: Директории с таким id не существует");

        directory.Icon = await _imageService.FileSaver(request.Icon, "C:\\Cloud\\Icon");
        directory.AtUpdate = DateTime.UtcNow;

        _repository.Update(directory);
        await _repository.SaveAsync();

        return _mapper.Map<BaseDirectoryResponse>(directory);
    }

    public async Task<BaseDirectoryResponse> Move(UpdatePathDirectoryRequest request)
    {
        var directory = await _repository.Get(request.Id);

        if (directory == null)
            throw new Exception("[Move || Directory Service]: Директории с таким id не существует");

        if (!Directory.Exists(request.NewPath))
            throw new Exception("[Move || Directory Service]: Директории по выбранному пути не существует");

        var parentDirectory = await _repository.GetByPath(request.NewPath);

        if (parentDirectory == null)
            throw new Exception(
                "[Move || Directory Service]: Директории в который вы хотите переместить директорию не существует");

        FilterDirectoryByName(directory.Name, request.NewPath);
        Directory.Move(directory.Path, request.NewPath + '\\' + directory.Name);

        directory.ParentId = parentDirectory.Id;
        directory.Path = request.NewPath + '\\' + directory.Name;
        directory.AtUpdate = DateTime.UtcNow;

        _repository.Update(directory);
        await _repository.SaveAsync();

        await UpdateSubDirectoryPath(directory.Id, directory.Path);

        return _mapper.Map<BaseDirectoryResponse>(directory);
    }

    public async Task<bool> Delete(Guid id)
    {
        var directory = await _repository.Get(id);

        if (directory == null)
            throw new Exception("[Delete || Directory service]: Директории с таким id не существует");
        
        _repository.Delete(directory);
        await _repository.SaveAsync();
        
        Directory.Delete(directory.Path, true);

        return true;
    }

    private bool FilterDirectoryByName(string name, string path)
    {
        if (!Directory.Exists(path))
            throw new Exception("[Filter Directory By Name || Directory Service]: Такой директории не существует");

        var directories = Directory.GetDirectories(path);

        foreach (var directory in directories)
            if (directory == name)
                throw new Exception(
                    "[Filter Directory By Name || Directory Service]: Папка с таким именем уже существует");

        return true;
    }

    private async Task UpdateSubDirectoryPath(Guid parentId, string parentPath)
    {
        var subDirectory = await _repository.GetSubDirectories(parentId);

        foreach (var directory in subDirectory)
        {
            if (directory.Path == Path.Combine(parentPath, directory.Name))
                throw new Exception("[Update sub directory path || Directory service]: ");

            directory.Path = Path.Combine(parentPath, directory.Name);
            directory.AtUpdate = DateTime.Now;

            if (Directory.Exists(directory.Path))
            {
                _repository.Update(directory);
                await _repository.SaveAsync();
            }

            await UpdateSubDirectoryPath(directory.Id, directory.Path);
        }
    }
}