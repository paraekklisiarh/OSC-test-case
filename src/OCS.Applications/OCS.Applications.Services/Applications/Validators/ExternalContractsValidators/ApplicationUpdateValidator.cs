using FluentValidation;
using OCS.Applications.Contracts.Requests;

namespace OCS.Applications.Services.Applications.Validators.ExternalContractsValidators;
/// <summary>
/// Валидация заявки при редактировании
/// </summary>
public class ApplicationUpdateValidator : AbstractValidator<UpdateApplicationDto>
{
    public ApplicationUpdateValidator()
    {
        RuleFor(x => x).Must(
            x => x.Activity is not null
                 || string.IsNullOrEmpty(x.Name) is false
                 || string.IsNullOrEmpty(x.Description) is false
                 || string.IsNullOrEmpty(x.Outline) is false
        ).WithMessage("нельзя отредактировать заявку не указав хотя бы еще одно поле помимо идентификатора пользователя");
    }
}