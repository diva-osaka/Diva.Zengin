using System.Collections.ObjectModel;
using System.Reflection;
using CsvHelper.Configuration.Attributes;
using Diva.Zengin.Converters;
using Diva.Zengin.Records;
using TypeConverterAttribute = CsvHelper.Configuration.Attributes.TypeConverterAttribute;

namespace Diva.Zengin;

internal static class PropertyAnalyzer
{
    private static readonly string NumberTypeConverterBaseName =
        typeof(NumberTypeConverter<>).Name.Split('`')[0];

    /// <summary>
    /// プロパティのIndex属性・TypeConverter属性からインデックス値と文字数を取得する
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>Key: index, Value: length</returns>
    public static Dictionary<int, int> GetIndexToLengthMap<T>() where T : IRecord
    {
        var result = new Dictionary<int, int>();
        var type = typeof(T);
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            // Index attribute
            var indexAttr = property.GetCustomAttribute<IndexAttribute>();
            if (indexAttr == null)
                continue;

            var index = indexAttr.Index;
            var length = 1; // default length

            // TypeConverter attribute
            var typeConverterAttributeData = property.CustomAttributes
                .FirstOrDefault(a => a.AttributeType == typeof(TypeConverterAttribute));

            if (typeConverterAttributeData is not { ConstructorArguments.Count: > 0 })
            {
                result[index] = length;
                continue;
            }

            var converterTypeArg = typeConverterAttributeData.ConstructorArguments[0];
            var converterType = converterTypeArg.Value as Type;
            var typeName = converterType?.Name ?? string.Empty;

            if (typeName.Contains(nameof(JapaneseEraDateTypeConverter)))
            {
                length = 6;
            }
            else if ((typeName.Contains(NumberTypeConverterBaseName) ||
                      typeName == nameof(CharacterTypeConverter))
                     && typeConverterAttributeData.ConstructorArguments.Count > 1)
            {
                // Get the length from the constructor arguments
                // The first argument is the type, the second and subsequent are the constructor args
                if (typeConverterAttributeData.ConstructorArguments[1].Value is
                    ReadOnlyCollection<CustomAttributeTypedArgument>
                    {
                        Count: > 0
                    } constructorArgs)
                {
                    length = constructorArgs.Select(arg =>
                            (int)(arg.Value ??
                                  throw new ArgumentException($"{typeName} requires an integer length parameter")))
                        .First();
                }
            }

            result[index] = length;
        }

        return result;
    }
}