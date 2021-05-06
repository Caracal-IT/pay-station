using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Caracal.Framework.Xml {
    public static class XmlExtensionMethods {
        public static string ToXml(this object objEntity) {
            var sb = new StringBuilder();
            
            new XmlSerializer(objEntity.GetType())
                .Serialize(new StringWriter(sb), objEntity);
            
            return sb.ToString();
        }

        public static string ToXml(this object entity, string xslt) {
            var writer = new StringWriter();
            var transform = new XslCompiledTransform();
            transform.Load(xslt.CreateReader());
            transform.Transform(entity.CreateReader(), null, writer);
            
            return writer.ToString();
        }
        
        private static XmlTextReader CreateReader(this string xml) => 
            new (new StringReader(xml));

        private static XmlTextReader CreateReader(this object entity) => 
            entity.ToXml().CreateReader();
    }
}