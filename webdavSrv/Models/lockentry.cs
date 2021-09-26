using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "lockentry", Namespace = "DAV:")]
    public class lockentry
    {
        [XmlElement(ElementName = "lockscope", Namespace = "DAV:")]
        public lockscope lockscope { get; set; }

        [XmlElement(ElementName = "locktype", Namespace = "DAV:")]
        public locktype locktype { get; set; }
    }
}