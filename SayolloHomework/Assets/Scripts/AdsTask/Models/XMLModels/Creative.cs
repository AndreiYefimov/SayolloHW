using System.Xml.Serialization;

namespace AndriiYefimov.SayolloHW.Models.XMLModels
{
    [XmlRoot(ElementName = "Creative")]
    public class Creative
    {
        [XmlElement(ElementName = "Linear")] public Linear Linear { get; set; }
    }
}