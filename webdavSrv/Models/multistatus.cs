using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "multistatus", Namespace = "DAV:")]
    public class multistatus
    {
        //[XmlArray(ElementName = "response", Namespace = "DAV:")]
        //[XmlArrayItem(ElementName = "response", Type = typeof(response), Namespace = "DAV:")]
        [XmlElement("response")]
        public List<response> response { get; set; }
    }
}