using Devices.Domain.Enums;

namespace Devices.Api.Models
{
    public record DeviceCreateDto(string Name, string Brand, DeviceState State);
    public record DeviceUpdateDto(string? Name, string? Brand, DeviceState? State);
    public record DeviceResponseDto(Guid Id, string Name, string Brand, DeviceState State, DateTimeOffset CreationTime);
}
