using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PhotoExhibiter.Presentation.ViewModels.Filters
{
    public class ValidTime : ValidationAttribute
    {
        public override bool IsValid (object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact (Convert.ToString (value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            return (isValid);
        }
    }
}