using log4net.Core;
using alkemyumsa.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace alkemyumsa.DataAccess.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context) {
        _context = context;
    }

    // realiza una consulta asincrónica a la base de datos para obtener todas las entidades de tipo T y devolverlas como una lista. 
    public virtual async Task<List<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual Task<T?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<bool> Insert(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        
    }

    public virtual Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Delete(int id )
    {
        throw new NotImplementedException();
    }
}
