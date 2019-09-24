using System.ComponentModel;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        private static T ConvertFrom<T>(object value)
        {
            var type = typeof(T);

            if (type.IsInstanceOfType(value))
            {
                return (T) value;
            }

            return (T) TypeDescriptor.GetConverter(type).ConvertFrom(value);
        }
        
        public static T Get<T>(this IDictionary<string, object> self, string key) 
            => ConvertFrom<T>(self[key]);
        
        public static T Get<T>(this IDictionary<string, object> self, string key, T @default)
        {
            if (self.ContainsKey(key))
            {
                return ConvertFrom<T>(self[key]);
            }

            return @default;
        }
        
        public static T Get<T>(this IDictionary<string, object> self, string key, Func<T> @default)
        {
            if (@default == null)
            {
                throw new ArgumentNullException(nameof(@default));
            }
            
            if (self.ContainsKey(key))
            {
                return ConvertFrom<T>(self[key]);
            }

            return @default.Invoke();
        }
        
        public static object Get(this IDictionary<string, object> self, string key, object @default)
        {
            if (self.ContainsKey(key))
            {
                return self[key];
            }

            return @default;
        }

        public static object Get(this IDictionary<string, object> self, string key, Func<object> @default)
        {
            if (@default == null)
            {
                throw new ArgumentNullException(nameof(@default));
            }
            
            if (self.ContainsKey(key))
            {
                return self[key];
            }

            return @default.Invoke();
        }
    }
}
