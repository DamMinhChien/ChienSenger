using Application.Interfaces.Common;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(AppDbContext db)
    {
        _db = db;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null) await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null) await _transaction.RollbackAsync();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => _db.SaveChangesAsync(cancellationToken);
}