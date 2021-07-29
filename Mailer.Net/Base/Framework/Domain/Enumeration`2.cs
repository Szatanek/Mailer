using System.Collections.Generic;
using System.Linq;

namespace Framework.Domain
{
    public abstract class Enumeration<TValue, TEnumeration> : ValueObject<TValue, TEnumeration>
        where TEnumeration : Enumeration<TValue, TEnumeration>
    {
        protected Enumeration(TValue value, string name) 
            : base(value)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString() => Name;

        public static IEnumerable<TEnumeration> GetAll()
        {
            var enumerationType = typeof(TEnumeration);
            var properties = enumerationType.GetProperties()
                .Where(p => p.CanRead && !p.CanWrite && enumerationType.IsAssignableFrom(p.PropertyType))
                .ToArray();

            return properties.Select(f => f.GetValue(null)).Cast<TEnumeration>();
        }

        protected static TEnumeration Get(TValue value)
        {
            return GetAll().FirstOrDefault(v => v.Value.Equals(value));
        }
    }
}
