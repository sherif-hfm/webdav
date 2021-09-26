using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "supportedlock", Namespace = "DAV:")]
    public class supportedlock
    {
        [XmlElement(ElementName = "lockentry", Namespace = "DAV:")]
        public List<lockentry> lockentry { get; set; }
    }
}