﻿using FluentValidation.Validators;
using System;

namespace FluentValidationBR.Extensions
{
    public class UriValidator : PropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string;

            if (value != null && !Uri.TryCreate(value, UriKind.Absolute, out _))
            {
                context.MessageFormatter.AppendArgument(nameof(UriValidator), value);
                return false;
            }

            return true;
        }

        protected override string GetDefaultMessageTemplate()
        => @"'{PropertyName}' não é uma URI válida.";
    }
}