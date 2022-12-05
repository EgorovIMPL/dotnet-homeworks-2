using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel(this IHtmlHelper helper)
    {
        var model = helper.ViewData.Model;
        var properties = helper.ViewData.ModelMetadata.ModelType.GetProperties();
        if (model is null)
            return GetRequest(properties);
        return PostRequest(properties, model);
    }
    
    private static IHtmlContent PostRequest(PropertyInfo[] properties, object? model)
    {
        var builder = new HtmlContentBuilder();

        foreach (var property in properties)
        {
            var validationAttributes = property.GetCustomAttributes<ValidationAttribute>();
            var propertyValue = property.GetValue(model);

            builder.AppendHtmlLine($"<div>{GetForm(property)}");
            
            foreach (var validationAttribute in validationAttributes)
            {
                if (!validationAttribute.IsValid(propertyValue))
                {
                    builder.AppendHtmlLine(
                        $"{GetLabel($"{property.Name}", string.Empty)}" +
                        $"<span>{validationAttribute.ErrorMessage}</span>");
                }
            }
            
            builder.AppendHtmlLine("<br></div>");
        }

        return builder;
    }


    private static IHtmlContent GetRequest(PropertyInfo[] properties)
    {
        var builder = new HtmlContentBuilder();

        foreach (var property in properties)
            builder.AppendHtmlLine($"<div>{GetForm(property)}<br></div>");

        return builder;
    }

    private static string GetForm(PropertyInfo property)
    {
        var name = property.Name;
        var type = property.PropertyType;
        var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
        var labelContent = GetLabelContent(property.Name, displayAttribute);

        if (type.IsEnum)
        {
            var enumNames = type.GetEnumNames();
            var selectTag = $"<select>{string.Join("", enumNames.Select(el => $"<option value=\"{el}\">{el}</option>").ToArray())}</select>";;
            
            return $"{GetLabel($"{name}", labelContent)}<br>{selectTag}<br>";
        }
        
        var contentType = type == typeof(string) ? "text" : "number";

        return $"{GetLabel($"{name}", labelContent)}<br>" +
                   $"<input id=\"{name}\" name=\"{name}\" type=\"{contentType}\">";
    }

    private static string GetLabel(string attribute, string content) => 
        $"<label for=\"{attribute}\">{content}</label>";
    
    private static string GetLabelContent(string propertyName, DisplayAttribute? attribute)
    {
        if (attribute is null)
            return Regex.Replace(propertyName, "([A-Z])", " $1",RegexOptions.Compiled).Trim();
        return attribute.Name;
    }
} 