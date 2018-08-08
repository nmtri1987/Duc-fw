using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Reflection;
using System.ComponentModel;
using System.Linq.Expressions;

    public static class HtmlExtensions
    {

        private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type realModelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(realModelType);
            if (underlyingType != null)
            {
                realModelType = underlyingType;
            }
            return realModelType;
        }

        private static readonly SelectListItem[] SingleEmptyItem = new[] { new SelectListItem { Text = "", Value = "" } };

        public static string GetEnumDescription<TEnum>(TEnum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if ((attributes != null) && (attributes.Length > 0))
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static MvcHtmlString EnumDropDownListForProp<TModel, TProperty, TEnum>
            (this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            TEnum enumList,
            string optionLabel,
            object htmlAttributes)
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //Type enumType = GetNonNullableModelType(metadata);
            //IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

            IEnumerable<SelectListItem> items = ToSelectList(typeof(TEnum), ((int)Enum.Parse(typeof(TEnum), enumList.ToString())).ToString());

            // If the enum is nullable, add an 'empty' item to the collection
            if (metadata.IsNullableValueType)
                items = SingleEmptyItem.Concat(items);

            return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
        }
        public static SelectList ToSelectList(Type enumType, string selectedItem)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in Enum.GetValues(enumType))
            {
                FieldInfo fi = enumType.GetField(item.ToString());
                var attribute = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
                var title = attribute == null ? item.ToString() : ((DescriptionAttribute)attribute).Description;
                var listItem = new SelectListItem
                {
                    Value = ((int)item).ToString(),
                    Text = title,
                    Selected = selectedItem == ((int)item).ToString()
                };
                items.Add(listItem);
            }

            return new SelectList(items, "Value", "Text", selectedItem);
        }


    }

