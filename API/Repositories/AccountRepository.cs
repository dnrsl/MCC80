using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BookingDbContext _context;

    public AccountRepository(BookingDbContext context)
    {
        _context = context;
    }

    public Account? Create(Account account)
    {
        try
        {
            _context.Set<Account>()
                    .Add(account);
            _context.SaveChanges();
            return account;
        }
        catch
        {
            return null;
        }
    }

    public bool Delete(Account account)
    {
        try
        {
            _context.Set<Account>()
                    .Remove(account);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public Account? GetByGuid(Guid guid)
    {
        var data = _context.Set<Account>()
                            .Find(guid);
        _context.ChangeTracker.Clear();
        return data;
    }

    public IEnumerable<Account> GetAll()
    {
        return _context.Set<Account>()
                        .ToList();
    }

    public bool Update(Account account)
    {
        try
        {
            _context.Entry(account)
                    .State = EntityState.Modified;
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
