using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.Infrastructure.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device?> GetAsync(Guid id, CancellationToken ct = default);
        Task<List<Device>> GetAllAsync(CancellationToken ct = default);
        Task<List<Device>> GetByBrandAsync(string brand, CancellationToken ct = default);
        Task<List<Device>> GetByStateAsync(DeviceState state, CancellationToken ct = default);
        Task AddAsync(Device device, CancellationToken ct = default);
        Task DeleteAsync(Device device, CancellationToken ct = default);
        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
