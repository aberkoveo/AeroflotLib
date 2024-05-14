using FluentValidation;
using Integra.Domain.ContentCapture;

namespace Integra.WebAPI.Controllers.ContentCapture.Validation
{
    public class ContentBatchValidator : AbstractValidator<ContentBatch>
    {
        public ContentBatchValidator()
        {
            RuleFor(batch => batch.Name).NotEmpty();
            RuleFor(batch => batch.ProjectId).NotEmpty();
            RuleFor(batch => batch.BatchTypeId).NotEmpty();
            RuleFor(batch => batch.Name).NotEmpty();
            RuleFor(batch => batch.OwnerId).NotEmpty();
            RuleFor(batch => batch.RegistrationParameters).NotNull();

            RuleFor(batch => batch.RegistrationParameters)
                .Must(parameters => parameters.ContainsKey("Станция сканирования"))
                .WithMessage("Не заполнен параметр \"Станция сканирования\"");

            RuleFor(batch => batch.RegistrationParameters)
                .Must(parameters => parameters.ContainsKey("Сканировщик"))
                .WithMessage("Не заполнен параметр \"Сканировщик\"");


        }
    }
}
