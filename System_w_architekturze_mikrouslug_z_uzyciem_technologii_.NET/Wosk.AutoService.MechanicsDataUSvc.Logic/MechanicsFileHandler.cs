using System.Diagnostics;
using System.Text.Json;
using System.Xml;
using Wosk.AutoService.MechanicsDataUSvc.Model;
using System.Xml.Schema;

namespace Wosk.AutoService.MechanicsDataUSvc.Logic
{
    public class MechanicsFileHandler
    {
        private const string networkNamespace = "net";
        private const string networkSchema = "http://tempuri.org/mechanics.xsd";

        #region File reading
        public Mechanic[] ReadMechanics(string filename, string xsdFilename="")
        {
            switch (GetExtension(filename))
            {
                case ".json":
                    return ReadMechanicsJson(filename);
                case ".xml":
                    return ReadMechanicsXml(ReadXML(filename, xsdFilename));
                default:
                    throw new ArgumentOutOfRangeException(nameof(filename), $"{nameof(filename)} extension type is not supported");
            }
        }

        private Mechanic[]? ReadMechanicsJson(string filename)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Mechanic[]? mechanics = JsonSerializer.Deserialize<Mechanic[]>(File.ReadAllText(filename), options);
            return mechanics;
        }

        private Mechanic[] ReadMechanicsXml(XmlDocument xmlDoc)
        {
            List<Mechanic> mechanics = new List<Mechanic>();

            XmlNamespaceManager xmlNamespaceManager = GetXmlNamespaceManager(xmlDoc);

            string xPath = String.Format("/net:Mechanics/net:Mechanic");

            XmlNodeList? linkXmlNodes = xmlDoc.DocumentElement?.SelectNodes(xPath, xmlNamespaceManager);

            Debug.Assert(linkXmlNodes != null && linkXmlNodes.Count > 0);

            foreach (XmlElement xmlElement in linkXmlNodes)
            {
                mechanics.Add(ConvertToMechanic(xmlElement));
            }

            return mechanics.ToArray();
        }


        #endregion

        #region File writing

        public void WriteMechanics(string fileName, Mechanic[] mechanics) 
        {
            switch (GetExtension(fileName))
            {
                case ".json":
                     WriteMechanicsJson(fileName, mechanics);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(fileName), $"{nameof(fileName)} extension type is not supported");
            }
        }

        private void WriteMechanicsJson(string filename, Mechanic[] mechanics)
        {
            File.WriteAllText(filename, JsonSerializer.Serialize(mechanics));
        }
        #endregion

        #region Inner functions
        private  string GetExtension(string filename)
        {
            string extension;

            if (filename.EndsWith(".json"))
            {
                extension = ".json";
            }
            else if (filename.EndsWith(".xml"))
            {
                extension = ".xml";
            }
            else extension = ".txt";

            return extension;
        }

        private  XmlDocument ReadXML(string xmlFilename, string xsdFilename)
        {
            Debug.Assert(condition: !String.IsNullOrWhiteSpace(xmlFilename) && !String.IsNullOrWhiteSpace(xsdFilename));

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas.Add(null, xsdFilename);
            settings.ValidationEventHandler += ValidationCallback;

            // This will stop if it detects validation error 
            using (XmlReader xmlReader = XmlReader.Create(xmlFilename, settings))
            {
                xmlDoc.Load(xmlReader);
            }

            return xmlDoc;
        }

        private  XmlNamespaceManager GetXmlNamespaceManager(XmlDocument networkXmlDocument)
        {
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(networkXmlDocument.NameTable);

            xmlNamespaceManager.AddNamespace(networkNamespace, networkSchema);

            return xmlNamespaceManager;
        }

        private  void ValidationCallback(object? sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation error: {0}", e.Message);
        }

        private Mechanic ConvertToMechanic(XmlElement xmlElement)
        {
            int mechanicId = int.Parse(xmlElement.GetAttribute("MechanicId"));
            string name = xmlElement.GetAttribute("Name");
            string surname = xmlElement.GetAttribute("Surname");
            string pesel = xmlElement.GetAttribute("Pesel");
            string repairsIdsTemp = xmlElement.GetAttribute("RepairsIds");

            List<int> repairsIds = new List<int>();
            foreach (string repairId in repairsIdsTemp.Split(",")){ 
                repairsIds.Add(Int32.Parse(repairId));
            }
            return new Mechanic(mechanicId, name, surname, repairsIds.ToArray(), pesel);
        }
        #endregion

    }
}
