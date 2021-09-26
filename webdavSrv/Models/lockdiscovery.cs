using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "lockdiscovery", Namespace = "DAV:")]
    public class lockdiscovery
    {
        [XmlElement(ElementName = "activelock", Namespace = "DAV:")]
        public activelock activelock { get; set; }
    }
}