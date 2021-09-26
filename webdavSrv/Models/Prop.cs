using System;
using System.Xml.Serialization;

namespace webdavSrv
{
    [Serializable]
    [XmlRoot(ElementName = "prop", Namespace = "DAV:")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class Prop
    {
        [XmlElement(ElementName = "getcontenttype", Namespace = "DAV:", Type = typeof(string))]
        public object getcontenttype { get; set; }

        [XmlElement(ElementName = "getlastmodified", Namespace = "DAV:", Type = typeof(string))]
        public object getlastmodified { get; set; }

        [XmlElement(ElementName = "lockdiscovery", Namespace = "DAV:")]
        public lockdiscovery lockdiscovery { get; set; }

        [XmlElement(ElementName = "ishidden", Namespace = "DAV:", Type = typeof(string))]
        public object ishidden { get; set; }

        [XmlElement(ElementName = "supportedlock", Namespace = "DAV:")]
        public supportedlock supportedlock { get; set; }
        
        [XmlElement(ElementName = "getetag", Namespace = "DAV:", Type = typeof(string))]
        public object getetag { get; set; }

        [XmlElement(ElementName = "displayname", Namespace = "DAV:", Type = typeof(string))]
        public object displayname { get; set; }

        [XmlElement(ElementName = "getcontentlanguage", Namespace = "DAV:", Type = typeof(string))]
        public object getcontentlanguage { get; set; }

        [XmlElement(ElementName = "getcontentlength", Namespace = "DAV:", Type = typeof(string))]
        public object getcontentlength { get; set; }

        [XmlElement(ElementName = "iscollection", Namespace = "DAV:", Type = typeof(string))]
        public object iscollection { get; set; }

        [XmlElement(ElementName = "creationdate", Namespace = "DAV:", Type = typeof(string))]
        public object creationdate { get; set; }

        [XmlElement(ElementName = "resourcetype", Namespace = "DAV:")]
        public resourcetype resourcetype { get; set; }




        [XmlElement(ElementName = "supported-report-set", Namespace = "DAV:")]
        public object supportedReportSet { get; set; }

        [XmlElement(ElementName = "quota-available-bytes", Namespace = "DAV:")]
        public object quotaAvailableBytes { get; set; }

        [XmlElement(ElementName = "quota-used-bytes", Namespace = "DAV:")]
        public object quotaUsedBytes { get; set; }

        #region Microsoft has some own elements in DAV namespace
        [XmlElement(ElementName = "contentclass", Namespace = "DAV:", Type = typeof(string))]
        public object contentclass { get; set; }

        [XmlElement(ElementName = "defaultdocument", Namespace = "DAV:", Type = typeof(string))]
        public object defaultdocument { get; set; }

        [XmlElement(ElementName = "href", Namespace = "DAV:", Type = typeof(string))]
        public object href { get; set; }

        

       

        [XmlElement(ElementName = "isreadonly", Namespace = "DAV:", Type = typeof(string))]
        public object isreadonly { get; set; }

        [XmlElement(ElementName = "isroot", Namespace = "DAV:", Type = typeof(string))]
        public object isroot { get; set; }

        [XmlElement(ElementName = "isstructureddocument", Namespace = "DAV:", Type = typeof(string))]
        public object isstructureddocument { get; set; }

        [XmlElement(ElementName = "lastaccessed", Namespace = "DAV:", Type = typeof(string))]
        public object lastaccessed { get; set; }

        [XmlElement(ElementName = "name", Namespace = "DAV:", Type = typeof(string))]
        public object name { get; set; }

        [XmlElement(ElementName = "parentname", Namespace = "DAV:", Type = typeof(string))]
        public object parentname { get; set; } 
        #endregion

    }
}