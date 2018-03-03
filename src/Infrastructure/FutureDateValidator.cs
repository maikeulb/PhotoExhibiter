using System;
using System.Globalization;
using FluentValidation.Validators;

namespace PhotoExhibiter.Infrastructure
{
    public class FutureDateValidator : PropertyValidator
    {
        public FutureDateValidator () : base ("{PropertyName} must be in the future") { }

        protected override bool IsValid (PropertyValidatorContext context)
        {
            var date = context.PropertyValue;
            DateTime dateTime;

            var isValid = DateTime.TryParseExact (Convert.ToString (date),
                "d MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            if (isValid & dateTime > DateTime.Now)
                return true;

            return false;
        }
    }
}