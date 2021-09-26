using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "propfind", Namespace = "DAV:")]
    public class PropfindReq
    {
        [XmlElement(ElementName = "prop", Namespace = "DAV:")]
        public Prop prop { get; set; }
    }

    
}