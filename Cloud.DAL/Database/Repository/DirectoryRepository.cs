using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class DirectoryRepository : IDirectoryRepository
{
    private readonly DatabaseContext _db;

    public DirectoryRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<CustomDirectory?> Get(Guid id)
    {
        return await _db.Directories
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<CustomDirectory?> GetByPath(string path)
    {
        return await _db.Directories
            .FirstOrDefaultAsync(i => i.Path == path);
    }

    public async Task<ICollection<CustomDirectory>> GetByName(string name, Guid userId)
    {
        return await _db.Directories
            .Where(i => i.Name == name && i.OwnerId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<CustomDirectory>> GetAllUserDirectories(Guid userId)
    {
        return await _db.Directories
            .Where(i => i.OwnerId == userId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<CustomDirectory>> GetSubDirectories(Guid id)
    {
        return await _db.Directories
            .Where(i => i.ParentId == id)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CustomDirectory> Create(CustomDirectory entity)
    {
        await _db.Directories.AddAsync(entity);
        return entity;
    }

    public CustomDirectory Update(CustomDirectory entity)
    {
        _db.Directories.Update(entity);
        return entity;
    }

    public async Task<bool> Delete(CustomDirectory entity)
    {
        await CascadeDeleteDirectory(entity.Id);

        _db.Directories.Remove(entity);
        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    private async Task CascadeDeleteDirectory(Guid id)
    {
        var children = await _db.Directories
            .Where(i => i.ParentId == id).ToListAsync();

        foreach (var child in children)
        {
            await CascadeDeleteDirectory(child.Id);

            _db.Directories.Remove(child);
        }
    }
}