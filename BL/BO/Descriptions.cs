using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            foreach (PropertyInfo item in t.GetType().GetProperties())
            {
                if (!(item.GetValue(t, null) is string) && item.GetValue(t, null) is IEnumerable<T>)
                {
                    foreach (var item2 in (IEnumerable<T>)item.GetValue(t, null))
                        str += item2.ToString();
                }
                str += "\n" + item.Name

                    + ": " + item.GetValue(t, null);
            }
            return str;
        }
    }
}
