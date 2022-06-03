using System.Xml.Serialization;

namespace AndriiYefimov.SayolloHW.Models.XMLModels
{
    [XmlRoot(ElementName = "InLine")]
    public class InLine
    {
        [XmlElement(ElementName = "Error")] public string Error { get; set; }

        [XmlElement(ElementName = "Creatives")]
        public Creatives Creatives { get; set; }
    }
}