using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Test.Selenium.Automation.DataUtilities
{
    public class XmlUtility
    {
        private static XDocument _xmlFile;

        private static XDocument LoadXml(string filepath)
        {
            filepath += ".xml";

            if (File.Exists(filepath) || string.IsNullOrEmpty(filepath))
            {
                _xmlFile = XDocument.Load(filepath);
            }

            throw new Exception(string.Format("Path {0} is not valid", filepath));
        }

        private static string GetNodeValue(string nodeName)
        {
            var xelement = _xmlFile.Descendants().SingleOrDefault(x => x.Name == nodeName);

            if (xelement == null)
            {
                throw new Exception(string.Format("Couldn't find the node '{0}' in the xml '{1}'", nodeName, _xmlFile));
            }

            var locatorTypeAndValue = xelement.Descendants().Select(subElement => subElement.Value).SingleOrDefault();
            return locatorTypeAndValue;
        }
    }
}
