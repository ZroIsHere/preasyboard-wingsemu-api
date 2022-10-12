using System;
using System.Collections.Generic;
using System.Xml;

namespace noswebapp.Tmp
{
    public class XMLHandler
    {

        static void Main(string[] args)
        {
            
        }


        public static void WriteXML(Request req)
        {
            
            var writer =
                new System.Xml.Serialization.XmlSerializer(typeof(Request));

            String path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, req);
            file.Close();
        }

        public static string xmlReadId()
        {
            // First write something so that there is something to read ...  
           

            // Now we can read the serialized book ...  
            var reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Request));
            var file = new System.IO.StreamReader(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml");
            var overview = (Request)reader.Deserialize(file);
            file.Close();
            
            Console.WriteLine(overview.id);
            return Convert.ToString(overview.id);

        }
        public static string xmlReadChallenge()
        {
            // First write something so that there is something to read ...  


            // Now we can read the serialized book ...  
            var reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Request));
            var file = new System.IO.StreamReader(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml");
            var overview = (Request)reader.Deserialize(file);
            file.Close();

            Console.WriteLine(overview.challenge);

            return overview.challenge;
        }

        public static string xmlReadTimestamp()
        {
            // First write something so that there is something to read ...  


            // Now we can read the serialized book ...  
            var reader =
                new System.Xml.Serialization.XmlSerializer(typeof(Request));
            var file = new System.IO.StreamReader(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//SerializationOverview.xml");
            var overview = (Request)reader.Deserialize(file);
            file.Close();

            Console.WriteLine(overview.timestamp);

            return overview.timestamp;
        }
    }

}