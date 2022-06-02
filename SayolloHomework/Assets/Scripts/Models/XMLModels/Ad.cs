using System.Xml.Serialization;

namespace AndriiYefimov.SayolloHW.Models.XMLModels
{
    [XmlRoot(ElementName = "Ad")]
    public class Ad
    {
        [XmlElement(ElementName = "InLine")] public InLine InLine { get; set; }
    }
}