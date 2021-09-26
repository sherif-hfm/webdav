using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "lockscope", Namespace = "DAV:")]
    public class lockscope
    {

        [XmlElement(ElementName = "exclusive", Namespace = "DAV:")]
        public object exclusive { get; set; }

        [XmlElement(ElementName = "shared", Namespace = "DAV:")]
        public object shared { get; set; }
    }
}