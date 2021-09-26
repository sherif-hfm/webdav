using System;
using System.Xml.Serialization;
namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "activelock", Namespace = "DAV:")]
    public class activelock
    {
        [XmlElement(ElementName = "locktype", Namespace = "DAV:")]
        public locktype locktype { get; set; }

        [XmlElement(ElementName = "lockscope", Namespace = "DAV:")]
        public lockscope lockscope { get; set; }

        [XmlElement(ElementName = "depth", Namespace = "DAV:")]
        public string depth { get; set; }

        [XmlElement(ElementName = "owner", Namespace = "DAV:")]
        public hrefInfo owner { get; set; }

        [XmlElement(ElementName = "timeout", Namespace = "DAV:")]
        public string timeout { get; set; }

        [XmlElement(ElementName = "locktoken", Namespace = "DAV:")]
        public hrefInfo locktoken { get; set; }

        [XmlElement(ElementName = "lockroot", Namespace = "DAV:")]
        public hrefInfo lockroot { get; set; }
    }
}