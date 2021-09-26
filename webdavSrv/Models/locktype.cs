using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "locktype", Namespace = "DAV:")]
    public class locktype
    {
        [XmlElement(ElementName = "write", Namespace = "DAV:")]
        public object write { get; set; }
    }
}