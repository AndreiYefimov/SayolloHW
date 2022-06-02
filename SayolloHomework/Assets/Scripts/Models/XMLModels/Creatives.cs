using System.Xml.Serialization;

namespace AndriiYefimov.SayolloHW.Models.XMLModels
{
    [XmlRoot(ElementName = "Creatives")]
    public class Creatives
    {
        [XmlElement(ElementName = "Creative")] public Creative Creative { get; set; }
    }
}