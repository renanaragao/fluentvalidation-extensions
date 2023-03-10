using FluentValidation;
using FluentValidation.Validators;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FluentValidationBR.Extensions
{
    public class IsUniqueValidator<T, TProperty, TKey> : PropertyValidator<T, IEnumerable<TProperty>>
    {
        private readonly Expression<Func<TProperty, TKey>> keySelector;
        private readonly string propertyNameKeySelector;
        private readonly Expression<Func<T, IEnumerable<TProperty>>> otherExpression;
        private string messageTemplate;

        public override string Name { get; }

        public IsUniqueValidator(Expression<Func<T, IEnumerable<TProperty>>> expression, Expression<Func<TProperty, TKey>> keySelector) : this(keySelector)
        {
            otherExpression = expression;
        }

        public IsUniqueValidator(Expression<Func<TProperty, TKey>> keySelector)
        {
            this.keySelector = keySelector;
            Name = nameof(IsUniqueValidator<T, TProperty, TKey>);
            propertyNameKeySelector = GetPropertyName(keySelector.Body);
        }

        private static string GetPropertyName(Expression expression) => ((MemberExpression)expression).Member.Name;

        protected override string GetDefaultMessageTemplate(string erroCode) => messageTemplate;

        public override bool IsValid(ValidationContext<T> context, IEnumerable<TProperty> value)
        {
            if (otherExpression == null)
            {
                messageTemplate = $@"'{propertyNameKeySelector.Humanize(LetterCasing.Title)}' deve ser único.";

                return value?.GroupBy(keySelector.Compile()).All(x => x.Count() == 1) ?? true; ;
            }

            var propertyNameOtherList = GetPropertyName(otherExpression.Body);

            messageTemplate = $@"Existem elementos com a propriedade '{propertyNameKeySelector.Humanize(LetterCasing.Title)}' igual entre a lista '{{PropertyName}}' e '{propertyNameOtherList.Humanize(LetterCasing.Title)}'.";

            var otherList = (IEnumerable<TProperty>)context.InstanceToValidate.GetType().GetProperty(propertyNameOtherList).GetValue(context.InstanceToValidate);

            return value?.Concat(otherList ?? Enumerable.Empty<TProperty>()).GroupBy(keySelector.Compile()).All(x => x.Count() == 1) ?? true;
        }
    }

    public class IsUniqueValidator<T, TProperty> : PropertyValidator<T, IEnumerable<TProperty>>
    {
        public override string Name { get; }

        public IsUniqueValidator()
        {
            Name = nameof(IsUniqueValidator<T, TProperty>);
        }

        private static string GetPropertyName(Expression expression) => ((MemberExpression)expression).Member.Name;

        protected override string GetDefaultMessageTemplate(string erroCode)
        => @"'{PropertyName}' não deve conter elementos duplicados.";


        public override bool IsValid(ValidationContext<T> context, IEnumerable<TProperty> value)
        => value?.GroupBy(x => x).All(x => x.Count() == 1) ?? true;
    }
}
