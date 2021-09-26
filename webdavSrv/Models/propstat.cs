using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "propstat", Namespace = "DAV:")]
    public class propstat
    {
        [XmlElement(ElementName = "status", Namespace = "DAV:")]
        public string status { get; set; }

        [XmlElement(ElementName = "prop", Namespace = "DAV:")]
        public Prop prop { get; set; }

        [XmlElement(ElementName = "error", Namespace = "DAV:")]
        public object error { get; set; }

        [XmlElement(ElementName = "responsedescription", Namespace = "DAV:")]
        public object responsedescription { get; set; }
    }
}