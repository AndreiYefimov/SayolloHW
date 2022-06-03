using System.Xml.Serialization;

namespace AndriiYefimov.SayolloHW.Models.XMLModels
{
    [XmlRoot(ElementName = "MediaFiles")]
    public class MediaFiles
    {
        [XmlElement(ElementName = "MediaFile")]
        public string MediaFile { get; set; }
    }
}