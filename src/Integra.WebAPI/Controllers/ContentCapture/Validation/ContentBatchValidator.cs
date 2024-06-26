﻿using FluentValidation;
using Integra.Domain.ContentCapture;

namespace Integra.WebApi.Controllers.ContentCapture.Validation
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
                .WithMessage("Отсутствует параметр \"Станция сканирования\"");

            RuleFor(batch => batch.RegistrationParameters)
                .Must(parameters => parameters.ContainsKey("Дата сканирования"))
                .WithMessage("Отсутствует параметр \"Дата сканирования\"");

            RuleFor(batch => batch.RegistrationParameters)
                .Must(parameters => parameters.ContainsKey("Сканировщик"))
                .WithMessage("Отсутствует параметр \"Сканировщик\"");

        }
    }

    public class ContentBatchBase64Validator : ContentBatchValidator
    {
        public ContentBatchBase64Validator()
        {
            RuleFor(batch => batch.Base64DocumentFiles)
                .NotNull()
                .WithMessage("Отсутствут вложения документов в BASE64"); ;
        }
    }


}
