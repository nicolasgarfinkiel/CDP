using System.Configuration;

namespace CartaDePorte.Configuration
{
    public static class ConfigurationHelper
    {
        public static T GetSection<T>(string sectionName)
        {
            return (T)ConfigurationManager.GetSection(sectionName);
        }

        public static T GetSection<T>()
        {
            return (T)ConfigurationManager.GetSection(typeof(T).Name);
        }
    }
}
