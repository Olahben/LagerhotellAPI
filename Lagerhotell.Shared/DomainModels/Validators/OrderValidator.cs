﻿namespace LagerhotellAPI.Models.DomainModels.Validators;

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrdreId er obligatorisk");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("BrukerId er obligatorisk");
        RuleFor(x => x.OrderPeriod.OrderDate).NotEmpty().WithMessage("Startdato er obligatorisk");
        RuleFor(x => x.OrderPeriod.EndDate).NotEmpty().WithMessage("Sluttdato er obligatorisk");
        RuleFor(x => x.OrderPeriod.OrderDate).LessThan(x => x.OrderPeriod.EndDate).WithMessage("Start");
        RuleFor(x => x.StorageUnitId).NotEmpty().WithMessage("LagerenhetId er obligatorisk");
        RuleFor(x => x.Status).NotNull().WithMessage("Status er obligatorisk");
        RuleFor(x => x.CustomInstructions).MaximumLength(500).WithMessage("Tilleggsinformasjon kan ikke være lengre enn 500 bokstaver");
    }
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<DomainModels.Order>.CreateWithOptions((DomainModels.Order)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}
