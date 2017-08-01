using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;
using Mustava.Extensions;

namespace Mustava.Helper
{
    public static class XmlUtils
    {
        private static readonly XmlWriterSettings WriterSettings = 
            new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, Encoding = Encoding.UTF8};

        private static readonly XmlSerializerNamespaces Namespaces = 
            new XmlSerializerNamespaces(new[] { new XmlQualifiedName("", "") });


        public static string RemoveNonvalidXmlChars(string text)
        {
            if (text.IsNullOrEmpty())
                return string.Empty;
            var stringBuilder = new StringBuilder();

            foreach (var t in text)
            {
                if (IsValidXmlChar(t))
                    stringBuilder.Append(t);
            }

            return stringBuilder.ToString();
        }

        public static T  XmltoDtoConverter<T>(this string xml)
        {
            try
            {
                var xmlReader = new XmlTextReader(new StringReader("<?xml version='1.0' encoding='utf-8' ?>"
                    + xml.Trim()));

                return (T)(new XmlSerializer(typeof(T)).Deserialize(xmlReader));
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string toXml<T>(this T obj)
        {
            var xml = serializeSingle(obj);

            xml = Regex.Replace(xml, @"([+]|[-])\d\d:\d\d", "", RegexOptions.Multiline);

            return xml;
        }

        public static bool IsValidXmlChar(char c)
        {
            return
                Char.IsDigit(c) ||
                Char.IsLetter(c) ||
                Char.IsWhiteSpace(c) ||
                Char.IsControl(c);
        }

        private static string serializeSingle<T>(T obj)
        {
            var sb = new StringBuilder();
            
            using (var writer = XmlWriter.Create(sb, WriterSettings))
            {
                var serializer = new XmlSerializer(obj.GetType());

                serializer.Serialize(writer, obj, Namespaces);

                return sb.ToString();
            }
        }

    }
}