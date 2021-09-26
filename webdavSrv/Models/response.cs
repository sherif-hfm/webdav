using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "response", Namespace = "DAV:")]
    public class response
    {
        [XmlElement(ElementName = "href", Namespace = "DAV:")]
        public string href { get; set; }

        [XmlElement(ElementName = "propstat", Namespace = "DAV:")]
        public propstat propstat { get; set; }
    }
}