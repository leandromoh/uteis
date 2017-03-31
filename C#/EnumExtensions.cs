using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Common
{
    public static class EnumExtensions
    {
        private static Dictionary<Enum, string> Descriptions = new Dictionary<Enum, string>();
        private static Dictionary<Type, Dictionary<int, string>> AllEnumDescriptions = new Dictionary<Type, Dictionary<int, string>>();

        static EnumExtensions()
        {
            foreach (Type type in GetAllTypesInheritedFrom<Enum>())
            {
                AllEnumDescriptions[type] = GetDescriptionDictionary(type);
            }
        }

        private static List<Type> GetAllTypesInheritedFrom<T>()
        {
            var type = typeof(T);

            var temp = typeof(EnumExtensions)
                        .Assembly
                        .GetTypes()
                        .Where(t => type.IsAssignableFrom(t) && t != type)
                        .ToList();

            return temp;
        }

        private static Dictionary<int, string> GetDescriptionDictionary(Type enumType)
        {
            var descriptionDictionary = new Dictionary<int, string>();

            foreach (Enum e in Enum.GetValues(enumType))
            {
                descriptionDictionary[Convert.ToInt32(e)] = Descriptions[e] = e.GetDescriptionInAttribute(enumType);
            }

            return descriptionDictionary;
        }

        private static string GetDescriptionInAttribute(this Enum e, Type enumType)
        {
            FieldInfo field = enumType.GetField(e.ToString());
            object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return customAttributes.Length > 0 ? (customAttributes[0] as DescriptionAttribute).Description : e.ToString();
        }

        public static string GetDescription(this Enum e)
        {
            string value;

            if (Descriptions.TryGetValue(e, out value))
            {
                return value;
            }

            Type type = e.GetType();

            AllEnumDescriptions[type] = GetDescriptionDictionary(type);

            return Descriptions[e];
        }

        public static Dictionary<int, string> EnumToDictionary<TEnum>()
            where TEnum : struct
        {
            var type = typeof(TEnum);
            if (!type.IsEnum) throw new ArgumentException("T must be an Enum");

            Dictionary<int, string> dictionary;

            if (AllEnumDescriptions.TryGetValue(type, out dictionary))
            {
                return dictionary;
            }

            return (AllEnumDescriptions[type] = GetDescriptionDictionary(type));
        }


        // source code: http://stackoverflow.com/a/40203664
        public static Expression<Func<TSource, int>> DescriptionOrder<TSource, TEnum>(this Expression<Func<TSource, TEnum>> source)
            where TEnum : struct
        {
            var enumType = typeof(TEnum);
            if (!enumType.IsEnum) throw new InvalidOperationException();

            var body = ((TEnum[])Enum.GetValues(enumType))
                .OrderBy(value => (value as Enum).GetDescription())
                .Select((value, ordinal) => new { value, ordinal })
                .Reverse()
                .Aggregate((Expression)null, (next, item) => next == null ? (Expression)
                    Expression.Constant(item.ordinal) :
                    Expression.Condition(
                        Expression.Equal(source.Body, Expression.Constant(item.value)),
                        Expression.Constant(item.ordinal),
                        next));

            return Expression.Lambda<Func<TSource, int>>(body, source.Parameters[0]);
        }
    }
}
