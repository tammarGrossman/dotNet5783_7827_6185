
using System.Reflection;

namespace BO
{
    /// <summary>
    /// the function prints objects
    /// </summary>
    static class Descriptions
    {
        public static string Description<T>(T t)
        {
            string str = "";
            foreach (var property in t.GetType().GetProperties())
            {
                if (!(property.GetValue(t, null) is IEnumerable<Object>))
                    str += "\n" + property.Name + ":" + property.GetValue(t, null) + "\n";
                else
                {
                    str += "\n" + property.Name + ":\n";
                    foreach (var item in (IEnumerable<object>)property.GetValue(t, null))
                        str += item;
                }
            }
            return str;
        }
    }
}
