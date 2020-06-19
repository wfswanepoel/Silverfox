using System;
using System.Collections.Generic;
using System.Text;
using Application.Infrastructure.Validation.Interfaces;
using FluentValidation;

namespace Application.Infrastructure.Validation.RequestDate
{
    public class RequireDateValidator : AbstractValidator<IRequireDate>
    {
        public RequireDateValidator()
        {
            RuleFor(date => date.Date)
                .GreaterThan(DateTime.MinValue)
                .LessThan(DateTime.MaxValue);
        }
    }
}
