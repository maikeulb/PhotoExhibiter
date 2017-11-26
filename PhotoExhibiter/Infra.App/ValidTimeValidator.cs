using System;
using System.Globalization;
using FluentValidation.Validators;

namespace PhotoExhibiter.Infra.App
{
    public class ValidTimeValidator : PropertyValidator
    {
        public ValidTimeValidator () : base ("{PropertyName} is not valid") { }

        protected override bool IsValid (PropertyValidatorContext context)
        {
            var time = context.PropertyValue;
            DateTime dateTime;

            var isValid = DateTime.TryParseExact (Convert.ToString (time),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            if (isValid)
                return true;

            return false;
        }
    }
}
