using System;
using System.Xml.Serialization;
namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "multistatus", Namespace = "DAV:")]
    public class PropfindRes : multistatus
    {
    }
}