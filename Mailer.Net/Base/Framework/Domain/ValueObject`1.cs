using System.Diagnostics;
using System.Linq;

namespace Framework.Domain
{
    [DebuggerStepThrough]
    public abstract class ValueObject<TValueObject>
    {
        public static bool operator ==(ValueObject<TValueObject> obj1, ValueObject<TValueObject> obj2)
        {
            return (obj1 is null && obj2 is null)
                || (!(obj1 is null) && obj1.Equals(obj2));
        }

        public static bool operator !=(ValueObject<TValueObject> obj1, ValueObject<TValueObject> obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            var props = typeof(TValueObject)
                .GetProperties()
                .Where(p => p.CanWrite)
                .ToArray();

            var hashCodes = props
                .Select(p => p.GetValue(this)?.GetHashCode() ?? 0);
            
            return hashCodes.Aggregate((result, value) => result ^ value);
        }

        public override bool Equals(object obj)
        {
            return !(obj is null)
                && obj.GetHashCode() == GetHashCode();
        }
    }
}