using FluentValidation;

namespace Devices.Api.Models
{
    public class DeviceCreateValidator : AbstractValidator<DeviceCreateDto>
    {
        public DeviceCreateValidator()
        {
            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            RuleFor(d => d.Brand)
                .NotEmpty().WithMessage("Brand is required.")
                .MaximumLength(100).WithMessage("Brand must not exceed 100 characters.");
            RuleFor(d => d.State)
                .IsInEnum().WithMessage("State must be a valid DeviceState.");
        }
    }
    
    public class DeviceUpdateValidator : AbstractValidator<DeviceUpdateDto>
    {
        public DeviceUpdateValidator()
        {
            When(d => d.Name != null, () =>
            {
                RuleFor(d => d.Name!)
                    .NotEmpty().WithMessage("Name cannot be empty.")
                    .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");
            });
            When(d => d.Brand != null, () =>
            {
                RuleFor(d => d.Brand!)
                    .NotEmpty().WithMessage("Brand cannot be empty.")
                    .MaximumLength(100).WithMessage("Brand must not exceed 100 characters.");
            });
            When(d => d.State != null, () =>
            {
                RuleFor(d => d.State!)
                    .IsInEnum().WithMessage("State must be a valid DeviceState.");
            });
        }
    }
}
