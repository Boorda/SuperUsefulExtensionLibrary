namespace SUELIB.JSONExtensions
{
    using Newtonsoft.Json;

    using System;

    public static class JsonExtensions
    {
        /// <summary>
        /// Attempts to serialize a .Net Object into a JSON string.
        /// </summary>
        /// <typeparam name="T">.Net Type to be converted to JSON.</typeparam>
        ///<param name="obj">The object to be serialized.</param>
        /// <param name="throwException">
        /// If <c>true</c> then the method will throw an exception on error, if <c>false</c> the method returns null.<para/>
        /// Optional (Default = false)
        /// </param>
        public static string SerializeAsJSON<T>(this T obj, bool throwException = false)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                if (throwException) { throw new Exception($"Could not deserialize the object.{Environment.NewLine}{ex}"); }
                return null;
            }
        }

        /// <summary>
        /// Attempts to deserialize a JSON string into a .Net Object.
        /// </summary>
        /// <typeparam name="T">.Net Type to be converted to.</typeparam>
        /// <param name="json">A JSON string.</param>
        /// <param name="throwException">
        /// If <c>true</c> then the method will throw an exception on error, if <c>false</c> the method returns and empty object.<para/>
        /// Optional (Default = false)
        /// </param>
        public static T DeserializeJSON<T>(this string json, bool throwException = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json); ;
            }
            catch (Exception ex)
            {
                if (throwException)
                {
                    throw new Exception($"Could not deserialize the object.{Environment.NewLine}{ex}");
                }
                return default(T);
            }
        }

    }
}
