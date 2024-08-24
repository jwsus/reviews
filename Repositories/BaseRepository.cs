using CourseReviewAPI.Data;
using Microsoft.EntityFrameworkCore;

public abstract class BaseRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected DbSet<T> DbSet;

    protected BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        DbSet = _context.Set<T>();
    }

    public virtual void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }
}
