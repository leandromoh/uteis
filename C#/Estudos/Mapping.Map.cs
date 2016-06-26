using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Mapper<TIn, TOut>
            where TIn : class
            where TOut : class
    {
        protected Dictionary<PropertyInfo, PropertyInfo> CompatibleProperties;
        protected Dictionary<PropertyInfo, PropertyInfo> CompatiblePropertiesWithDiffReferenceTypes;

        public Mapper(ICollection<string> includedProperties = null, ICollection<string> excludedProperties = null)
        {
            CompatibleProperties = new Dictionary<PropertyInfo, PropertyInfo>();
            CompatiblePropertiesWithDiffReferenceTypes = new Dictionary<PropertyInfo, PropertyInfo>();

            Func<PropertyInfo, bool> predicate;

            if (excludedProperties != null)
                predicate = prop => !excludedProperties.Contains(prop.Name);
            else if (includedProperties != null)
                predicate = prop => includedProperties.Contains(prop.Name);
            else
                predicate = prop => true;

            FilterProperties(typeof(TIn), typeof(TOut), predicate);
        }

        private void FilterProperties(Type inType, Type outType, Func<PropertyInfo, bool> predicate)
        {
            foreach (PropertyInfo outInfo in outType.GetProperties())
            {
                PropertyInfo inInfo = outInfo != null && outInfo.CanWrite ? inType.GetProperty(outInfo.Name) : null;

                if (inInfo != null && inInfo.CanRead && predicate(inInfo))
                {
                    if (inInfo.PropertyType.Equals(outInfo.PropertyType))
                        CompatibleProperties.Add(inInfo, outInfo);

                    else if (!inInfo.PropertyType.IsValueType && !outInfo.PropertyType.Equals(inInfo.PropertyType))
                        CompatiblePropertiesWithDiffReferenceTypes.Add(inInfo, outInfo);
                }
            }
        }

        public void Map(TIn input, TOut output)
        {
            if (input == null || output == null)
                return;

            foreach (var pair in CompatibleProperties)
                pair.Value.SetValue(output, pair.Key.GetValue(input, null), null);

            foreach (var pair in CompatiblePropertiesWithDiffReferenceTypes)
                Map(pair.Key.GetValue(input, null), pair.Value.GetValue(output, null));
        }

        public static void Map<TIn, TOut>(TIn input, TOut output, ICollection<string> includedProperties = null, ICollection<string> excludedProperties = null)
            where TIn : class
            where TOut : class
        {
            if (input == null || output == null)
                return;

            Type inType = input.GetType();
            Type outType = output.GetType();

            bool includedNotNull = includedProperties != null;
            bool excludedNotNull = excludedProperties != null;

            foreach (PropertyInfo info in inType.GetProperties())
            {
                PropertyInfo outfo = ((info != null) && info.CanRead)
                    ? outType.GetProperty(info.Name)
                    : null;

                if (outfo != null && outfo.CanWrite && (outfo.PropertyType.Equals(info.PropertyType)))
                {
                    if (excludedNotNull && excludedProperties.Contains(info.Name))
                        continue;

                    if (includedNotNull && includedProperties.Contains(info.Name))
                        outfo.SetValue(output, info.GetValue(input, null), null);
                    else if (!includedNotNull)
                        outfo.SetValue(output, info.GetValue(input, null), null);
                }

                if (!info.PropertyType.IsValueType && outfo != null && !outfo.PropertyType.Equals(info.PropertyType))
                {
                    Map(info.GetValue(input, null), outfo.GetValue(output, null));
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var userDTO = new UserDTO { Id = "A", FirstName = "Tore", LastName = "TheCoder", Address = new AddressDTO { Number = 20, Street = "Rua das Flores" } };
            var user = new User { Id = 5, FirstName = "Leandro", Address = new Address() };

            var mapper = new Mapper<UserDTO, User>(excludedProperties: new[] { "FirstName" });
            mapper.Map(userDTO, user);

            Console.WriteLine(user);

            Console.Read();
        }
    }



    public class User
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public Address Address { get; set; }

        public override string ToString()
        {
            return string.Format("{{ Id = {2}, FirstName = {0}, LastName = {1}, Address = {3} }}", FirstName, LastName, Id, Address);
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            return string.Format("{{ Number = {0}, Street = {1} }}", Number, Street);
        }
    }

    public class UserDTO
    {
        public string Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

        public AddressDTO Address { get; set; }
    }

    public class AddressDTO
    {
        public string Street { get; set; }
        public int Number { get; set; }
    }
}
