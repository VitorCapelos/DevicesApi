using Devices.Domain.Entities;
using Devices.Domain.Enums;
using FluentAssertions;

namespace Devices.Tests.Domain
{
    public class DeviceTest
    {
        [Fact]
        public void CantUpdateNameWhenInUse()
        {
            // Arrange
            Device device = new Device("Device1", "BrandA", DeviceState.InUse);

            // Act
            Action act = () => device.UpdateName("NewDeviceName");

            // Assert
            act.Should().Throw<InvalidOperationException>()
                .WithMessage("Cannot update the name of a device that is currently in use.");
        }

        [Fact]
        public void CreationTimeIsSetOnCreateAndImumutable()
        {
            Device device = new Device("Device1", "BrandA", DeviceState.Available);
            DateTimeOffset originalCt = device.CreationTime;
            device.UpdateState(DeviceState.Inactive);
            device.CreationTime.Should().Be(originalCt);
        }
    }
}
