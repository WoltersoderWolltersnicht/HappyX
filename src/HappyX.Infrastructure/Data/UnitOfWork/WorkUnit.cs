using HappyX.Domain.Internal;
using HappyX.Infrastructure.Data.EF;
using HappyX.Infrastructure.Data.Repositories;

namespace HappyX.Infrastructure.Data.UnitOfWork;

public class WorkUnit : IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly HappyXContext _context;
 
    private bool _disposed = false;
    
    private IBaseRepository<User> _userRepository; 
    private IBaseRepository<Mood> _moodRepository; 
    private IBaseRepository<Record> _recordRepository; 
    
    public WorkUnit(IServiceProvider serviceProvider, HappyXContext context)
    {
        _serviceProvider = serviceProvider;
        _context = context;
    }

    public IBaseRepository<User> UserRepository => _userRepository ??= _serviceProvider.GetRepository<User>(_context);
    
    public IBaseRepository<Mood> MoodRepository => _moodRepository ??= _serviceProvider.GetRepository<Mood>(_context);
    
    public IBaseRepository<Record> RecordRepository => _recordRepository ??= _serviceProvider.GetRepository<Record>(_context);
    
    public virtual async Task SaveAsync()
    {
        await _context?.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}