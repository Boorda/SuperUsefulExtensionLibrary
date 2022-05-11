namespace VaultDocumentImporter.XDocumentExtensions
{
    using SUELIB.CollectionExtensions;
    using SUELIB.StringExtensions;

    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Xml.Linq;

    public static class XDocumentExtensions
    {

        internal static Regex XmlDeclaration = new Regex("^(<\\?xml version=\"1.0\".+\\?>)", RegexOptions.IgnoreCase);

        public static int GenerateXmDocHashCode(this XDocument xDocument)
        {
            List<string> NodeList = new List<string>();
            NodeList.AddRange(xDocument.GetUniqueXmlElementNames());
            NodeList.AddRange(xDocument.GetUniqueXmlAttributeNames());
            NodeList.Sort();
            return NodeList.GetDelimitedStringFromCollection(':').GetHashCode();
        }

        public static List<string> GetUniqueXmlAttributeNames(this XDocument xDocument)
        {
            List<string> attNames = new List<string>();
            foreach (string name in xDocument.Root.Attributes().Select(x => x.Name.LocalName).Distinct().OrderBy(x => x))
            {
                _ = attNames.AddDistinct(name);
            }
            foreach (string name in xDocument.Root.Descendants().Attributes().Select(x => x.Name.LocalName).Distinct().OrderBy(x => x))
            {
                _ = attNames.AddDistinct(name);
            }
            return attNames;
        }

        public static List<string> GetUniqueXmlElementNames(this XDocument xDocument)
        {
            List<string> eleNames = new List<string>() { xDocument.Root.Name.LocalName };

            foreach (var name in xDocument.Root.Descendants().Select(x => x.Name.LocalName).OrderBy(x => x))
            {
                eleNames.AddDistinct(name);
            }
            return eleNames;
        }

        /// <summary>
        /// Check to see if the file contains an XML declaration.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="convert">If <c>true</c> then replaces the XML declaration with &lt;?xml version="1.0" encoding="utf-8"?&gt;. </param>
        /// <returns></returns>
        public static bool IsXmlFile(this string filePath, bool throwException = false)
        {
            var file = File.ReadAllText(filePath);
            // First see if we have a standard XML file.
            if (XmlDeclaration.IsMatch(file))
            {
                return true;
            }
            if (throwException)
            {
                string fileName = Path.GetFileName(filePath);
                throw new FileFormatException($"The file {fileName} is not a valid XML document.");
            }
            return false;
        }

        public static XDocument OpenXML(this string xmlPath)
        {
            return xmlPath.IsXmlFile() ? XDocument.Load(File.ReadAllText(xmlPath)) : null;
        }

    }
}
