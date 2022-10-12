using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using noswebapp.RequestEntities;

namespace noswebapp.Helpers;

public class XMLHelper
{
    static void Main(string[] args)
    {
         
    }

    public static void WriteXML(WebAuthRequest req)
    {
        var writer = new XmlSerializer(typeof(WebAuthRequest));

        string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
        using FileStream file = File.Create(path);

        writer.Serialize(file, req);
        file.Close();
    }

    public static WebAuthRequest GetXmlDeserialized()
    {
        return (WebAuthRequest)new XmlSerializer(typeof(WebAuthRequest)).Deserialize(new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/SerializationOverview.xml"));
    }
}