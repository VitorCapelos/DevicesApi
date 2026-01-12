using Devices.Api.Models;
using Devices.Domain.Entities;
using Devices.Domain.Enums;
using Devices.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Devices.Api.Controllers
{
    [ApiController]
    [Route("api/devices")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceRepository _deviceRepository;
        public DevicesController(IDeviceRepository deviceRepository) => _deviceRepository = deviceRepository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceResponseDto>>> GetAll(CancellationToken ct)
        {
            var devices = await _deviceRepository.GetAllAsync(ct);
            return Ok(devices.Select(Map));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DeviceResponseDto>> Get(Guid id, CancellationToken ct)
        {
            var device = await _deviceRepository.GetAsync(id, ct);
            if (device is null)
                return NotFound();
            return Ok(Map(device));
        }

        [HttpGet("by-brand/{brand}")]
        public async Task<ActionResult<IEnumerable<DeviceResponseDto>>> GetByBrand(string brand, CancellationToken ct)
        {
            var devices = await _deviceRepository.GetByBrandAsync(brand, ct);
            return Ok(devices.Select(Map));
        }

        [HttpGet("by-state/{state}")]
        public async Task<ActionResult<IEnumerable<DeviceResponseDto>>> GetByState(DeviceState state, CancellationToken ct)
        {
            var devices = await _deviceRepository.GetByStateAsync(state, ct);
            return Ok(devices.Select(Map));
        }

        [HttpPost]
        public async Task<ActionResult<DeviceResponseDto>> Create(DeviceCreateDto dto, CancellationToken ct)
        {
            var device = new Device(dto.Name, dto.Brand, dto.State);
            await _deviceRepository.AddAsync(device, ct);
            await _deviceRepository.SaveChangesAsync(ct);
            return CreatedAtAction(nameof(Get), new { id = device.Id }, Map(device));
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<DeviceResponseDto>> Put(Guid id, DeviceUpdateDto dto, CancellationToken ct)
        {
            var device = await _deviceRepository.GetAsync(id, ct);
            if (device is null)
                return NotFound();

            if (dto.Name is not null)
                device.UpdateName(dto.Name);

            if (dto.Brand is not null)
                device.UpdateBrand(dto.Brand);

            if (dto.State is not null)
                device.UpdateState(dto.State.Value);

            await _deviceRepository.SaveChangesAsync(ct);
            return Ok(Map(device));
        }

        [HttpPatch("{id:guid}")]
        public async Task<ActionResult<DeviceResponseDto>> Patch(Guid id, DeviceUpdateDto dto, CancellationToken ct)
        {
            return await Put(id, dto, ct);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            var device = await _deviceRepository.GetAsync(id, ct);
            if (device is null)
                return NotFound();

            if (device.State == DeviceState.InUse)
                return BadRequest("Cannot delete a device that is currently in use.");

            await _deviceRepository.DeleteAsync(device, ct);
            await _deviceRepository.SaveChangesAsync(ct);
            return NoContent();
        }

        private static DeviceResponseDto Map(Device d) =>
        new DeviceResponseDto(d.Id, d.Name, d.Brand, d.State, d.CreationTime);
    }
}
