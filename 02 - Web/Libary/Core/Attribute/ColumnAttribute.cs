using System.ComponentModel.DataAnnotations;
using System;
using System.Globalization;
namespace Biz.Core.Attribute
{
    public class ColumnAttribute: System.Attribute
    {
        public string DataType { get; set; }
        public string ActionLink { get; set; }
        public string Url { get; set; }
        public string ClassName { get; set; }
        public bool Hide { get; set; }
        public bool NotAllowSearch { get; set; }
    }

    public class ValidDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(Convert.ToString(value),
                "dd/MM/yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None,
                out dateTime);

            return isValid;
        }
    }
    //public class CheckDateAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {

    //        DateTime date;
    //        displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
    //        if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
    //        {
    //            return date;
    //        }
    //        else
    //        {
    //            bindingContext.ModelState.AddModelError(
    //                bindingContext.ModelName,
    //                string.Format("{0} is an invalid date format", value.AttemptedValue)
    //            );
    //        }
    //        // Validate your Date here
    //    }
    //}

}
