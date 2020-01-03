using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ConsoleApp2
{
    public class GenericRecordParser<T>
        where T : class
    {
        private readonly IEnumerable<string> _mappedColumns;
        private readonly HashSet<string> _columnsThatIfEmptyShouldParseObjectAsNull;

        private readonly Func<T, string[], T> _funcThatSetProperties;
        private readonly Func<T> _getNewInstance;

        public GenericRecordParser(IEnumerable<string> mappedColumns, IEnumerable<string> columnsThatIfEmptyShouldObjectParseAsNull = null)
        {
            foreach (var skipColumn in columnsThatIfEmptyShouldObjectParseAsNull ?? Enumerable.Empty<string>())
                if (!mappedColumns.Contains(skipColumn))
                    throw new ArgumentException($"Property '{skipColumn}' is not mapped in mappedColumns parameter");

            _mappedColumns = mappedColumns;
            _funcThatSetProperties = GetFuncThatSetProperties(mappedColumns);
            _columnsThatIfEmptyShouldParseObjectAsNull = columnsThatIfEmptyShouldObjectParseAsNull?.ToHashSet() ?? new HashSet<string>();
            _getNewInstance = CreateInstanceHelper.GetInstanceGenerator<T>(_mappedColumns);
        }
            
        public GenericRecordParser(
            IEnumerable<(int index, string propertyName)> mappedColumns, 
            IEnumerable<string> columnsThatIfEmptyShouldObjectParseAsNull = null)
            : this (FillEmptyIndexes(mappedColumns), columnsThatIfEmptyShouldObjectParseAsNull)
        {

        }
            
        private static IEnumerable<string> FillEmptyIndexes(IEnumerable<(int index, string propertyName)> mappedColumns)
        {
            var columns = mappedColumns.ToArray();

            if (!columns.Any())
                return Enumerable.Empty<string>();

            var lenght = columns.Max(x => x.index) + 1;
            var filledColumns = Enumerable.Repeat<string>(null, lenght).ToArray();

            foreach (var (index, propertyName) in columns)
                filledColumns[index] = propertyName;

            return filledColumns;
        }

        public T Parse(string line)
        {
            T obj = _getNewInstance();

            return Parse(obj, line);
        }

        public T Parse(T obj, string line)
        {
            var values = line.Split(';');

            var skipParse = _mappedColumns
                 .Zip(values, (propertyName, value) => (propertyName, value))
                 .Any(y => string.IsNullOrWhiteSpace(y.value) && _columnsThatIfEmptyShouldParseObjectAsNull.Contains(y.propertyName));

            return skipParse
                   ? null
                   : _funcThatSetProperties(obj, values);
        }

        private static Func<T, string[], T> GetFuncThatSetProperties(IEnumerable<string> mappedColumns)
        {
            ParameterExpression objectParameter = Expression.Variable(typeof(T), "x");
            ParameterExpression valueParameter = Expression.Variable(typeof(string[]), "values");

            MethodInfo isNullOrWhiteSpaceMethodInfo = GetIsNullOrWhiteSpaceMethodInfo();
            var assignsExpressions = new List<Expression>();
            var i = 0;

            foreach (var propertyName in mappedColumns)
            {
                Expression textValue = Expression.ArrayIndex(valueParameter, Expression.Constant(i++));

                if (propertyName is null)
                    continue;

                var propertyType = propertyName
                    .Split('.')
                    .Aggregate(typeof(T), (type, member) => type.GetMember(member)[0].GetUnderlyingType());

                var nullableUnderlyingType = Nullable.GetUnderlyingType(propertyType);
                var isPropertyNullable = nullableUnderlyingType != null;
                var propertyUnderlyingType = nullableUnderlyingType ?? propertyType;

                Expression valueToBeSetExpression = GetValueToBeSetExpression(
                                                        propertyUnderlyingType,
                                                        textValue);

                if (isPropertyNullable)
                {
                    valueToBeSetExpression =
                        WrapWithTernaryThatReturnsNullIfValueIsEmpty(
                            valueToBeSetExpression,
                            textValue,
                            propertyType,
                            isNullOrWhiteSpaceMethodInfo);
                }

                var assign = GetAssignExpression(valueToBeSetExpression,
                                           objectParameter,
                                           propertyName);

                assignsExpressions.Add(assign);
            }

            assignsExpressions.Add(objectParameter);

            var blockExpr = Expression.Block(typeof(T), new ParameterExpression[] { }, assignsExpressions);

            return Expression.Lambda<Func<T, string[], T>>(blockExpr, new[] { objectParameter, valueParameter }).Compile();
        }

        private static Expression GetValueToBeSetExpression(Type propertyType, Expression valueText)
        {
            if (propertyType == typeof(string))
                return valueText;

            if (propertyType.IsEnum)
                return GetEnumParseExpression(propertyType, valueText);

            return GetParseExpression(propertyType, valueText);
        }

        private static Expression GetAssignExpression(
            Expression valueToBeSetExpression,
            ParameterExpression objectParameter,
            string propertyName)
        {
            var memberExpression = propertyName
                .Split('.')
                .Aggregate((Expression)objectParameter, (body, member) => Expression.PropertyOrField(body, member));

            return Expression.Assign(memberExpression, valueToBeSetExpression);
        }

        private static Expression WrapWithTernaryThatReturnsNullIfValueIsEmpty(
            Expression valueToBeSetExpression,
            Expression valueText,
            Type propertyType,
            MethodInfo isNullOrWhiteSpaceMethodInfo)
        {
            ConditionalExpression conditional =
                Expression.Condition(
                    test: Expression.Call(isNullOrWhiteSpaceMethodInfo, valueText),
                    ifTrue: Expression.Convert(Expression.Constant(null), propertyType),
                    ifFalse: Expression.Convert(valueToBeSetExpression, propertyType));

            return conditional;
        }

        private static MethodInfo GetIsNullOrWhiteSpaceMethodInfo()
        {
            MethodInfo isNullOrWhiteSpaceMethodInfo =
                            typeof(string)
                                 .GetMethod(nameof(string.IsNullOrWhiteSpace),
                                  BindingFlags.Static | BindingFlags.Public,
                                  null,
                                  new Type[] { typeof(string) },
                                  null);

            return isNullOrWhiteSpaceMethodInfo;
        }

        private static object ParseEnum(Type enumType, string value, bool ignoreCase) =>
            Enum.Parse(enumType, value?.Replace(" ", string.Empty), ignoreCase);

        private static Expression GetEnumParseExpression(Type propertyType, Expression valueText)
        {
            MethodInfo methodParse =
                            typeof(GenericRecordParser<T>)
                                .GetMethod(nameof(ParseEnum),
                                           BindingFlags.Static | BindingFlags.NonPublic,
                                           null,
                                           new Type[]
                                           {
                                               typeof(Type),
                                               typeof(string),
                                               typeof(bool)
                                           },
                                           null);

            var parsedValue = Expression.Call(methodParse,
                                         Expression.Constant(propertyType),
                                         valueText,
                                         Expression.Constant(true));

            return Expression.Convert(parsedValue, propertyType);
        }

        private static Expression GetParseExpression(Type type, Expression valueText)
        {
            MethodInfo methodParse =
                            typeof(Convert)
                                .GetMethod(nameof(Convert.ChangeType),
                                           BindingFlags.Static | BindingFlags.Public,
                                           null,
                                           new Type[]
                                           {
                                               typeof(object),
                                               typeof(Type),
                                               typeof(CultureInfo)
                                           },
                                           null);

            var parsedValue = Expression.Call(methodParse,
                                         valueText,
                                         Expression.Constant(type),
                                         Expression.Constant(CultureInfo.InvariantCulture));

            return Expression.Convert(parsedValue, type);
        }
    }


    internal static class CreateInstanceHelper
    {
        public static Func<T> GetInstanceGenerator<T>(IEnumerable<string> mapped)
        {
            var root = new Node(typeof(T));

            foreach (var path in mapped)
                if (path != null)
                    root.AddPath(path);

            var newTObject = GetNewExpressionWithNestedMemberInit(root);

            var getNewInstance = Expression.Lambda<Func<T>>(newTObject).Compile();

            return getNewInstance;
        }

        private static MemberInitExpression GetNewExpressionWithNestedMemberInit(Node type)
        {
            var memberBinds = type
                .PropertiesToInitialize
                .Select(info =>
                    Expression.Bind(info.Value.MemberInfo,
                                    GetNewExpressionWithNestedMemberInit(info.Value)));

            var newExpression = GetNewExpressionFor(type.MemberType);

            var member = Expression.MemberInit(newExpression, memberBinds);

            return member;
        }

        private static NewExpression GetNewExpressionFor(Type objType)
        {
            ConstructorInfo ctor = objType
                .GetConstructors()
                .OrderBy(x => x.GetParameters().Length)
                .First();

            return
                Expression.New
                (
                    ctor,
                    ctor.GetParameters().Select
                    (
                        x =>
                            x.IsOptional
                                ? Expression.Convert(Expression.Constant(x.DefaultValue), x.ParameterType)
                                : x.ParameterType.IsValueType && Nullable.GetUnderlyingType(x.ParameterType) == null
                                    ? Expression.Default(x.ParameterType)
                                    : (Expression)GetNewExpressionFor(x.ParameterType)
                    )
                );
        }

        internal class Node
        {
            public readonly IDictionary<string, Node> PropertiesToInitialize = new Dictionary<string, Node>();

            public Node(Type path) => MemberType = path;
            public Node(MemberInfo prop) : this(prop.GetUnderlyingType())
            {
                MemberInfo = prop;
            }

            public Type MemberType { get; private set; }
            public MemberInfo MemberInfo { get; private set; }

            public void AddPath(string path)
            {
                // Parse into a sequence of parts.
                string[] parts = path.Split('.',
                    StringSplitOptions.RemoveEmptyEntries);

                // The current node.  Start with this.
                Node current = this;

                // Iterate through the parts.
                foreach (string part in parts)
                {
                    // The child node.
                    Node child;

                    // Does the part exist in the current node?  If
                    // not, then add.
                    if (!current.PropertiesToInitialize.TryGetValue(part, out child))
                    {
                        var prop = current.MemberType.GetMember(part)[0];
                        var childType = prop.GetUnderlyingType();

                        if (childType.IsValueType || childType == typeof(string))
                            return;

                        // Add the child.
                        child = new Node(prop);

                        // Add to the dictionary.
                        current.PropertiesToInitialize[part] = child;
                    }

                    // Set the current to the child.
                    current = child;
                }
            }
        }
    }

    public static class MemberExtensions
    {
        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException
                    (
                     "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }
    }
}
