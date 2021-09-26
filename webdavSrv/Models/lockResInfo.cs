using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "prop", Namespace = "DAV:")]
    public class lockResInfo : Prop
    {
        //[XmlElement(ElementName = "lockdiscovery", Namespace = "DAV:")]
        //public lockdiscovery lockdiscovery { get; set; }
    }

    

  
}