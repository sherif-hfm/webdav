using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "lockinfo", Namespace = "DAV:")]
    public class lockinfo
    {
        [XmlElement(ElementName= "lockscope", Namespace = "DAV:")]
        public lockscope lockscope  { get; set; }

        [XmlElement(ElementName = "locktype", Namespace = "DAV:")]
        public locktype locktype  { get; set; }

        [XmlElement(ElementName = "owner", Namespace = "DAV:")]
        public hrefInfo owner { get; set; }

    }

   

    

    
}