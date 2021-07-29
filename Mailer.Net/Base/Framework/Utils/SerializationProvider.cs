namespace Framework.Utils
{
    public abstract class SerializationProvider
    {
        public static SerializationProvider Current;

        public abstract string Serialize<T>(T element);

        public abstract T Deserialize<T>(string value);
    }
}
