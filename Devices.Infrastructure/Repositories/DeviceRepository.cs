using Microsoft.EntityFrameworkCore;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Infrastructure.Data;

namespace Devices.Infrastructure.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DevicesDbContext _db;
        public DeviceRepository(DevicesDbContext db) => _db = db;

        public Task<Device?> GetAsync(Guid id, CancellationToken ct = default) =>
            _db.Devices.FirstOrDefaultAsync(d => d.Id == id, ct);

        public Task<List<Device>> GetAllAsync(CancellationToken ct = default) =>
            _db.Devices.AsNoTracking().ToListAsync(ct);

        public Task<List<Device>> GetByBrandAsync(string brand, CancellationToken ct = default) =>
            _db.Devices.AsNoTracking().Where(d => d.Brand == brand).ToListAsync(ct);

        public Task<List<Device>> GetByStateAsync(DeviceState state, CancellationToken ct = default) =>
            _db.Devices.AsNoTracking().Where(d => d.State == state).ToListAsync(ct);

        public Task AddAsync(Device device, CancellationToken ct = default) =>
            _db.Devices.AddAsync(device, ct).AsTask();

        public Task DeleteAsync(Device device, CancellationToken ct = default)
        {
            _db.Devices.Remove(device);
            return Task.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken ct = default) =>
            _db.SaveChangesAsync(ct);
    }
}
