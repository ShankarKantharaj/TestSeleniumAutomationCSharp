using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Test.Sample.Application.Library.Constants;

namespace Test.Sample.Application.Library.DataUtilities
{
    public class XmlRepositorySingleton
    {
        private static IDictionary<string, XDocument> _moduleFilePath;

        private static readonly Lazy<XmlRepositorySingleton> _instance = new Lazy<XmlRepositorySingleton>(() =>
        {
            _moduleFilePath = LoadAllXmls();
            return new XmlRepositorySingleton();
        });

        public static XmlRepositorySingleton Instance
        {
            get { return _instance.Value; }
        }

        private XmlRepositorySingleton()
        {

        }

        public string[] GetLocatorTypeAndValue(string moduleName, string key)
        {
            try
            {
                var doc = _moduleFilePath[moduleName];
                var xelement = doc.Descendants().SingleOrDefault(x => x.Name == key);

                if (xelement == null)
                {
                    throw new Exception(string.Format("Couldn't find the Key '{0}' in the xml '{1}'", key, moduleName));
                }

                var locator = xelement.Descendants().Select(subElement => subElement.Name).SingleOrDefault();
                var locatorValue = xelement.Descendants().Select(subElement => subElement.Value).SingleOrDefault();

                if (locator != null && locatorValue != null)
                {
                    return new[] { locator.ToString(), locatorValue};
                }

                throw new Exception("Couldn't fetch the locator type and Value");
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static XDocument LoadXml(string filepath)
        {
            filepath += ".xml";

            if (File.Exists(filepath) || string.IsNullOrEmpty(filepath))
            {
                return XDocument.Load(filepath);
            }

            throw new Exception(string.Format("Path {0} is not valid", filepath));
        }

        private static Dictionary<string, XDocument> LoadAllXmls()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            return new Dictionary<string, XDocument>
            {
                {XmlConstants.HomePage, LoadXml(baseDirectory + "\\xml\\" + XmlConstants.HomePage)},
            };

        }
    }
}
