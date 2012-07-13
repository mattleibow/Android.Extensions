namespace Android.Extensions
{
    using System;

    public class JavaObjectWrapper : Java.Lang.Object
    {
        public readonly object Instance;

        public JavaObjectWrapper(object instance)
        {
            this.Instance = instance;
        }
    }

    public static class ObjectExtension
    {
        private const string ErrorMessage = "Unable to convert to .NET object. Only Java.Lang.Object created with .AsJavaObject() can be converted.";

        public static T AsNetObject<T>(this Java.Lang.Object value)
            where T : class
        {
            if (value == null)
            {
                return default(T);
            }

            var wrapperObject = value as JavaObjectWrapper;

            if (wrapperObject == null)
            {
                throw new InvalidOperationException(ErrorMessage);
            }

            return (T) wrapperObject.Instance;
        }

        public static Java.Lang.Object AsJavaObject<T>(this T value)
            where T : class
        {
            if (value == null)
            {
                return null;
            }

            var holder = new JavaObjectWrapper(value);

            return holder;
        }
    }
}