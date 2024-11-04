using Cloud.DAL.Database.Interface;
using Cloud.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Cloud.DAL.Database.Repository;

public class CompanyRepository : ICompanyRepository
{
    private readonly DatabaseContext _db;

    public CompanyRepository(DatabaseContext db)
    {
        _db = db;
    }

    public async Task<Company?> Get(Guid id)
    {
        return await _db.Companies
            .Include(c => c.Owner)
            .Include(c => c.UserCompany)!
            .ThenInclude(c => c.User)
            .Include(c => c.Roles)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Company?> GetByName(string name)
    {
        return await _db.Companies.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task<ICollection<Company>> SelectAll()
    {
        return await _db.Companies.AsNoTracking().ToListAsync();
    }

    public async Task<ICollection<Company>> Pagination(uint number, uint size)
    {
        return await _db.Companies
            .Skip(((int)number - 1) * (int)size)
            .Take((int)size)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Company> Create(Company company)
    {
        await _db.Companies.AddAsync(company);

        return company;
    }

    public Company Update(Company company)
    {
        _db.Companies.Update(company);

        return company;
    }

    public bool Delete(Company company)
    {
        _db.Companies.Remove(company);

        return true;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}