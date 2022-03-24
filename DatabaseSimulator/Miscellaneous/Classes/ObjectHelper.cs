using System.Reflection;
using DatabaseSimulator.Exceptions;

namespace DatabaseSimulator.Miscellaneous;

public static class ObjectHelper
{
    public static IEnumerable<string> GetCommonProperties<TSource, TTarget>(
        Type? typeOfSource = null,
        Type? typeOfTarget = null
    )
    {
        Type _typeOfSource = typeOfSource ?? typeof(TSource);
        Type _typeOfTarget = typeOfTarget ?? typeof(TTarget);

        IEnumerable<string> oldProperties = typeof(TSource)
            .GetProperties()
            .Select(prop => prop.Name);

        IEnumerable<string> newProperties = typeof(TTarget)
            .GetProperties()
            .Select(prop => prop.Name);

        return oldProperties.Intersect(newProperties);
    }

    public static TTarget CopyTo<TSource, TTarget>(
        TSource source,
        TTarget target,
        Type? typeOfSource = null,
        Type? typeOfTarget = null,
        IEnumerable<string>? commonProperties = null
    )
    {
        Type _typeOfSource = typeOfSource ?? typeof(TSource);
        Type _typeOfTarget = typeOfTarget ?? typeof(TTarget);

        IEnumerable<string> _commonProperties =
            commonProperties ?? GetCommonProperties<TSource, TTarget>(typeOfSource, typeOfTarget);

        foreach (string prop in _commonProperties)
        {
            var sourceVal = _typeOfSource.GetProperty(prop)?.GetValue(source);

            _typeOfTarget.GetProperty(prop)?.SetValue(target, sourceVal);
        }

        return target;
    }

    public static void Set<T>(T target, string property, object value)
    {
        PropertyInfo? targetProperty = typeof(T).GetProperty(property);

        if (ReferenceEquals(null, targetProperty))
        {
            throw new InexistantPropertyException("Error: Said property doesn't exists !");
        }

        if (!value.GetType().Equals(targetProperty.PropertyType))
        {
            throw new ValueTypeMismatchException("Error: Value type doesn't match");
        }

        // ! Test Me
        targetProperty.SetValue(target, value);
    }
    // ? public static void Nullify() {}

}
