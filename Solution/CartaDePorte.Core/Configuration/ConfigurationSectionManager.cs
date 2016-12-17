using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace CartaDePorte.Configuration
{
    public class ConfigurationSectionManager<T> : ConfigurationSection where T : class
    {
        private XmlSerializer serializer;
        private T configurationItem;

        public static T GetSection(string sectionName)
        {
            ConfigurationSectionManager<T> configurationSectionManager = (ConfigurationSectionManager<T>)ConfigurationManager.GetSection(sectionName);
            if (null == configurationSectionManager)
                return default(T);
            else
                return configurationSectionManager.configurationItem;
        }

        public static T GetSection(string sectionName, System.Configuration.Configuration configuration)
        {
            ConfigurationSectionManager<T> configurationSectionManager = (ConfigurationSectionManager<T>)configuration.GetSection(sectionName);
            if (null == configurationSectionManager)
                return default(T);
            else
                return configurationSectionManager.configurationItem;
        }

        protected override void Init()
        {
            base.Init();
            this.serializer = new XmlSerializer(typeof(T), new XmlRootAttribute(this.SectionInformation.Name));
        }

        protected override void DeserializeSection(XmlReader reader)
        {
            this.configurationItem = (T)this.serializer.Deserialize(reader);
        }

        protected override string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            XmlSectionWriter xmlSectionWriter = new XmlSectionWriter(new StringWriter());
            this.serializer.Serialize((XmlWriter)xmlSectionWriter, (object)this.configurationItem);
            return xmlSectionWriter.ToString();
        }

        protected override bool IsModified()
        {
            return true;
        }
    }
}
