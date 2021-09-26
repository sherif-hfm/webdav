using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "owner", Namespace = "DAV:")]
    public class hrefInfo
    {
        [XmlElement(ElementName = "href", Namespace = "DAV:")]
        public string href { get; set; }
    }
}