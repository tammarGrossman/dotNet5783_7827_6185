
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace Dal;

static class XmlTools
{
    const string s_dir = @"..\xml\";
    static XmlTools()
    {
        if (!Directory.Exists(s_dir))
            Directory.CreateDirectory(s_dir);

    }
    /// <summary>
    /// save list to xml 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="lst"></param>
    /// <param name="entity"></param>
    /// <exception cref="Exception"></exception>
    public static void SaveListToXMLSerializer<T>(List<T?> lst, string entity) where T: struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            using FileStream file = new(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
            XmlSerializer serializer = new(typeof(List<T?>));
            serializer.Serialize(file, lst);
        }
        catch (Exception ex)
        {
            throw new Exception("ex {0}", ex);
        }
    }
    /// <summary>
    /// load from xml to list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<T?> LoadListFromXMLSerializer<T>(string entity) where T : struct
    {
        string filePath = $"{s_dir + entity}.xml";
        try
        {
            if (!File.Exists(filePath))
                return new();
            using FileStream file = new(filePath, FileMode.Open);
            XmlSerializer x = new(typeof(List<T?>));
            return x.Deserialize(file) as List<T?> ?? new();
        }
        catch (Exception ex)
        {
            throw new Exception("ex {0}", ex);
        }

    }
}

