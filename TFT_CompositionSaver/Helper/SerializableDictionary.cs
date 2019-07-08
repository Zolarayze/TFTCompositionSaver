using System.Collections.Generic;
using System.Xml.Serialization;

namespace TFT_CompositionSaver.Helper
{
    [XmlRoot("Keybindings")]
    public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                object key = reader.GetAttribute("key");
                object value = reader.GetAttribute("value");

                this.Add((TKey)key, (TValue)value);
                reader.Read();
            }
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (TKey key in this.Keys)
            {
                writer.WriteStartElement("binding");

                writer.WriteAttributeString("key", key.ToString());
                writer.WriteAttributeString("value", this[key].ToString());

                writer.WriteEndElement();
            }
        }
        #endregion
    }
}
