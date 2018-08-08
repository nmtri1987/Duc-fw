using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;
namespace Biz.Core
{
    public class DecimalModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext,
                                         ModelBindingContext bindingContext)
        {
            string modelName = bindingContext.ModelName;
            string attemptedValue =
                bindingContext.ValueProvider.GetValue(modelName).AttemptedValue;

            if (bindingContext.ModelMetadata.IsNullableValueType
                    && string.IsNullOrWhiteSpace(attemptedValue))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(attemptedValue))
            {
                return decimal.Zero;
            }

            decimal value = decimal.Zero;
            Regex digitsOnly = new Regex(@"[^\d]", RegexOptions.Compiled);
            var numbersOnly = digitsOnly.Replace(attemptedValue, "");
            if (!string.IsNullOrWhiteSpace(numbersOnly))
            {
                var numbers = Convert.ToDecimal(numbersOnly);
                //value = (numbers / 100m);
                value = (numbers);

                return value;
            }
            else
            {
                if (bindingContext.ModelMetadata.IsNullableValueType)
                {
                    return null;
                }

            }

            return value;
         
        }
    }

    public class DateModelBiner : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext,
                                        ModelBindingContext bindingContext)
        {
            DateTime d = DateTime.Now;
            return d;
        }
    }
    public class DateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
        }
    }

    public class NullableDateTimeBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, value);

            return value == null
                ? null
                : value.ConvertTo(typeof(DateTime), new System.Globalization.CultureInfo("it-CH"));
        }
    }
    public class CustomDateModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (!string.IsNullOrEmpty(displayFormat) && value != null)
            {
                DateTime date;
                displayFormat = displayFormat.Replace("{0:", string.Empty).Replace("}", string.Empty);
                displayFormat = displayFormat.Replace("m", "M");
                if (DateTime.TryParseExact(value.AttemptedValue, displayFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format("{0} is an invalid date format", value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
    public class DateFiexedCultureModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            if (propertyDescriptor.PropertyType == typeof(DateTime?))
            {
                try
                {
                    var model = bindingContext.Model;
                    PropertyInfo property = model.GetType().GetProperty(propertyDescriptor.Name);

                    var value = bindingContext.ValueProvider.GetValue(propertyDescriptor.Name);

                    if (value != null)
                    {
                        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("it-CH");
                        var date = DateTime.Parse(value.AttemptedValue, cultureinfo);
                        property.SetValue(model, date, null);
                    }
                }
                catch
                {
                    bindingContext.ModelState.AddModelError(
                       bindingContext.ModelName,
                       string.Format("{0} is an invalid date format", bindingContext.ValueProvider.GetValue(bindingContext.ModelName))
                   );
                    //If something wrong, validation should take care
                }
            }
            else
            {
                base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
            }
        }
    }
}
