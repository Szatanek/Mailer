using System;
using System.Diagnostics;
using Framework.Validation;

namespace Framework.Domain
{
    [DebuggerStepThrough]
    public abstract class ValueObject<TValue, TObject> : ValueObject<TObject>
        where TObject : class
    {
        protected ValueObject(TValue value)
        {
            Value = value;
        }

        protected TValue Value { get; }

        public static explicit operator TValue(ValueObject<TValue, TObject> valueObject)
        {
            Guard.NotNull(valueObject);
            Guard.TypeOf<TObject>(valueObject);
            return valueObject.Value;
        }

        public static bool operator == (ValueObject<TValue, TObject>obj1, ValueObject<TValue, TObject> obj2)
        {
            return (obj1 is null && obj2 is null)
                || (!(obj1 is null) && obj1.Equals(obj2));
        }

        public static bool operator != (ValueObject<TValue, TObject> obj1, ValueObject<TValue, TObject> obj2)
        {
            return !(obj1 == obj2);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return !(obj is null)
                && obj.GetHashCode() == GetHashCode();
        }
    }
}