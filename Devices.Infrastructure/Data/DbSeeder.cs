using Devices.Domain.Entities;
using Devices.Domain.Enums;

namespace Devices.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(DevicesDbContext db)
        {
            if (!db.Devices.Any())
            {
                db.Devices.AddRange(
                    new Device("Router Example", "BrandSample", DeviceState.Available),
                    new Device("Switch TW1000", "TechWire", DeviceState.InUse),
                    new Device("Smartphone C90", "CordinalBrand", DeviceState.Inactive)
                );
                await db.SaveChangesAsync();
            }
        }
    }
}
