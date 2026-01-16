using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCase.Services.Xml
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Xml.Xsl;

    namespace TestCase.Services.Xml
    {
        public interface IXmlService
        {
            void TransformXml(string sourcePath, string xsltPath, string outputPath);
            T LoadXmlFile<T>(string filePath) where T : class;
        }

        public class XmlService : IXmlService
        {
            public void TransformXml(string sourcePath, string xsltPath, string outputPath)
            {
                if (!File.Exists(sourcePath))
                {
                    throw new FileNotFoundException($"Исходный файл не найден: {sourcePath}");
                }

                if (!File.Exists(xsltPath))
                {
                    throw new FileNotFoundException($"XSLT файл не найден: {xsltPath}");
                }

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(xsltPath);

                string directory = Path.GetDirectoryName(outputPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (XmlWriter writer = XmlWriter.Create(outputPath))
                {
                    xslt.Transform(sourcePath, writer);
                }
            }

            public T LoadXmlFile<T>(string filePath) where T : class
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException($"XML файл не найден: {filePath}");
                }

                var serializer = new XmlSerializer(typeof(T));

                using (var reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
        }
    }
}
