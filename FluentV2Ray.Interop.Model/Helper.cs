namespace FluentV2Ray.Interop.Model
{
    public class Helper
    {
        /// <summary>
        /// Gets a Nullable value, if it is null, then create a new object and assign to it, otherwise return the existing value;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T CacheGetter<T>(T? val) where T : new()
        {
            if (val is null)
                val = new T();
            return val;
        }
    }
}
