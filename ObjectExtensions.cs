namespace SUELIB.ObjectExtensions
{
    public static class ObjectExtensions
    {

        #region Not Implemented
        /////// <summary>
        /////// Holds any object instances created via the GetInstance method.
        /////// </summary>
        ////private static Dictionary<string, object> _instances = new Dictionary<string, object>();

        /////// <summary>
        /////// Code Not Finished, does not work.
        /////// </summary>
        ////public static dynamic GetInstance<T>(this T obj, string objectname)
        ////{
        ////    try
        ////    {
        ////        if (!_instances.ContainsKey(""))
        ////        {
        ////            // We need to create the new instance.

        ////        }
        ////        return null;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        ex.Assess();
        ////        return null;
        ////    }
        ////}

        #endregion

        public static bool IsTypeOf<T>(this object obj)
        {
            return obj.GetType() == typeof(T) ? true : false;
        }

        public static bool NotTypeOf<T>(this object obj)
        {
            return obj.GetType() != typeof(T) ? true : false;
        }
    }
}
