using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "prop", Namespace = "DAV:")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class resourcetype
    {
        [XmlElement(ElementName = "collection", Namespace = "DAV:")]
        public object collection { get; set; }
    }
}