using System.IO;
using System.Xml;

namespace CartaDePorte.Configuration
{
    public class XmlSectionWriter : XmlTextWriter
    {
        private bool skipNamespace = false;
        private XmlWriterSettings settings = new XmlWriterSettings();
        private StringWriter stringWriter;

        public XmlSectionWriter(StringWriter w)
            : base((TextWriter)w)
        {
            this.stringWriter = w;
        }

        public override string ToString()
        {
            return this.stringWriter.ToString();
        }

        public override void WriteDocType(string name, string pubid, string sysid, string subset)
        {
        }

        public override void WriteStartDocument()
        {
        }

        public override void WriteStartDocument(bool standalone)
        {
        }

        public override void WriteStartAttribute(string prefix, string localName, string ns)
        {
            if (prefix == "xmlns" && (localName == "xsi" || localName == "xsd"))
                this.skipNamespace = true;
            else
                base.WriteStartAttribute(prefix, localName, ns);
        }

        public override void WriteString(string text)
        {
            if (this.skipNamespace)
                return;
            base.WriteString(text);
        }

        public override void WriteEndAttribute()
        {
            if (this.skipNamespace)
                this.skipNamespace = false;
            else
                base.WriteEndAttribute();
        }
    }
}
