using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.ComponentModel;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using log4net;
using System.Xml.Serialization;
using System.Globalization;

namespace CartaDePorte.Common
{
    public static class Extensions
    {
        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
    }

    public static class Tools
    {
        private static string _numberDecimalSeparator = CultureInfo.CurrentUICulture.NumberFormat.NumberDecimalSeparator;

        public static decimal ConvertToDecimal(object value)
        {
            string svalue = "";
            if (value != null)
            {
                if (Tools._numberDecimalSeparator.Equals("."))
                    svalue = value.ToString().Replace(",", ".");
                if (Tools._numberDecimalSeparator.Equals(","))
                    svalue = value.ToString().Replace(".", ",");
            }
            return Tools.Value2<decimal>(svalue, 0);
        }

        public static object ChangeType(object value, Type tipo, object defaultvalue)
        {
            try
            {
                return ChangeType(value, tipo);
            }
            catch
            {
                return defaultvalue;
            }
        }

        public static object ChangeType(object valor, Type tipo)
        {
            object value;

            if (valor == DBNull.Value)
                value = null;
            else
                value = valor;

            if (tipo == null)
                throw new ArgumentNullException("tipo");

            if (tipo.IsGenericType && tipo.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                    return null;
                tipo = (new NullableConverter(tipo)).UnderlyingType;
            }

            if (tipo == typeof(DateTime))
            {
                if (value.ToString().Length == 8 && value.ToString().IndexOf("/") == -1 && value.ToString().IndexOf("-") == -1)
                {
                    return DateTime.ParseExact(value.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
                }
                else
                    return Convert.ChangeType(value, typeof(DateTime));
            }

            if (tipo == typeof(string))
            {
                if (value != null)
                    return value.ToString();
                else
                    return null;
            }
            else
                return Convert.ChangeType(value, tipo);
        }

        public static T Value2<T>(object value, T defaultvalue)
        {
            T retvalue = default(T);
            retvalue = (T)ChangeType(value, typeof(T), defaultvalue);
            return retvalue;
        }

        public static T Value2<T>(object value)
        {
            T retvalue = default(T);
            try
            {
                retvalue = (T)ChangeType(value, typeof(T));
            }
            catch (System.FormatException ex)
            {
                throw ex;
            }
            return retvalue;
        }


        public static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public static string GetResourceFile(string resourceName)
        {
            return GetResourceFile(Assembly.GetExecutingAssembly(), resourceName);
        }

        public static string GetResourceFile(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static ILog _logger;

        public static ILog Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = LogManager.GetLogger("CartaDePorte");
                    log4net.Config.XmlConfigurator.Configure();
                }
                return _logger;
            }
        }

        private static Dictionary<Type, XmlSerializer> _serializers = new Dictionary<Type, XmlSerializer>();

        public static string XmlSerialize(object data)
        {
            XmlSerializer xmlSer = Tools.XmlSerializerGetFor(data.GetType());
            StringWriter sw = new StringWriter();
            xmlSer.Serialize(XmlWriter.Create(sw), data);

            return sw.ToString();
        }

        public static string XmlSerialize(object data, Type type)
        {
            XmlSerializer xmlSer = Tools.XmlSerializerGetFor(type);
            StringWriter sw = new StringWriter();
            xmlSer.Serialize(XmlWriter.Create(sw), data);

            return sw.ToString();
        }

        private static XmlSerializer XmlSerializerGetFor(Type typeOfT)
        {
            if (!Tools._serializers.ContainsKey(typeOfT))
            {
                var xmlAttributes = new XmlAttributes();
                var xmlAttributeOverrides = new XmlAttributeOverrides();
                
                xmlAttributes.XmlType = new XmlTypeAttribute
                {
                    Namespace = ""
                };

                xmlAttributes.Xmlns = false;
                var types = new List<Type> { typeOfT, typeOfT.BaseType };

                foreach (var property in typeOfT.GetProperties())
                {
                    types.Add(property.PropertyType);
                }

                types.RemoveAll(t => t.ToString().StartsWith("System."));

                foreach (var type in types)
                {
                    xmlAttributeOverrides.Add(type, xmlAttributes);
                }

                var newSerializer = new XmlSerializer(typeOfT, xmlAttributeOverrides);
                Tools._serializers.Add(typeOfT, newSerializer);
            }
            return Tools._serializers[typeOfT];
        }

        public static T XmlDeserialize<T>(string xml)
        {
            XmlSerializer xmlSer = Tools.XmlSerializerGetFor(typeof(T));
            return (T)xmlSer.Deserialize(new StringReader(xml));
        }

        public static bool ValidarPatente(String texto)
        {
            MatchCollection matchesPatenteOld = Regex.Matches(texto, @"^[A-ZÑ]{3}\d{3}$");
            MatchCollection matchesPatenteNew = Regex.Matches(texto, @"^[A-ZÑ]{2}\d{3}[A-ZÑ]{2}$");
            return matchesPatenteOld.Count > 0 || matchesPatenteNew.Count > 0;
        }
    }
}
