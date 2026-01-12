using Devices.Domain.Enums;

namespace Devices.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; set; }
        public string Name { get; private set; } = default!;
        public string Brand { get; private set; } = default!;
        public DeviceState State { get; private set; }
        public DateTimeOffset CreationTime { get; private set; }

        public Device(string name, string brand, DeviceState state)
        {
            Id = Guid.NewGuid();
            Name = name;
            Brand = brand;
            State = state;
            CreationTime = DateTimeOffset.UtcNow;
        }

        public void UpdateName(string name)
        {
            if (State == DeviceState.InUse) 
                throw new InvalidOperationException("Cannot update the name of a device that is currently in use.");

            Name = name;
        }

        public void UpdateBrand(string brand)
        {
            if (State == DeviceState.InUse) 
                throw new InvalidOperationException("Cannot update the brand of a device that is currently in use.");

            Brand = brand;
        }

        public void UpdateState(DeviceState state) => State = state;
    }
}
