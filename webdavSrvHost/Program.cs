using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using webdavSrv;

namespace webdavSrvHost
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("webdavSrvHost");
        static void Main(string[] args)
        {
            StartSers();

            //var result= Directory.GetFileSystemEntries(@"C:\temp\");
            //var fileInfo = new System.IO.FileInfo(@"C:\temp");
            //var result = System.Web.MimeMapping.GetMimeMapping(@"C:\temp\doc1.docx");
            //SerializeLockReq();
            //SerializeLockRes();
            //SerializePropfindRes();
            //Deserialize();


        }
        private static void SerializeLockReq()
        {
            //lockReqInfo info = new lockReqInfo();

            //info.lockscope = new lockscope() { exclusive = new object() };
            //info.locktype = new locktype() { write = new object() };
            //info.owner = new hrefInfo() { href = @"DESKTOP-FP4SN02\user" };

            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("D", "DAV:");
            //XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            //StringWriter textWriter = new StringWriter();
            //xmlSerializer.Serialize(textWriter, info, ns);
            //var result = textWriter.ToString();
        }

        private static void SerializeLockRes()
        {
            lockResInfo info = new lockResInfo();

            info.lockdiscovery   = new lockdiscovery();
            info.lockdiscovery.activelock = new activelock();
            info.lockdiscovery.activelock.lockscope = new lockscope() { exclusive = new object() };
            info.lockdiscovery.activelock.locktype = new locktype() { write = new object() };
            info.lockdiscovery.activelock.depth = "0";
            info.lockdiscovery.activelock.owner = new hrefInfo() { href = @"DESKTOP-FP4SN02\user" };
            info.lockdiscovery.activelock.timeout = "Second-3600";
            info.lockdiscovery.activelock.locktoken = new hrefInfo() { href = @"opaquelocktoken:e9962eb2-5a53-49df-830a-14db5a7899fc.088101d7ad90a509" };
            info.lockdiscovery.activelock.lockroot = new hrefInfo() { href = @"http://localhost/doc1.docx" };

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("D", "DAV:");
            XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, info, ns);
            var result = textWriter.ToString();
        }

        private static void SerializePropfindRes()
        {
            PropfindRes info = new PropfindRes();
            info.response = new  List<response>();
            info.response[0] = new response();
            info.response[0].propstat = new propstat();
            //info.response[0].propstat.prop = new Prop() {ishidden="0" };

            info.response[1] = new response();
            info.response[1].propstat = new propstat();
            info.response[1].propstat.prop = new Prop();


            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("D", "DAV:");
            XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, info, ns);
            var result = textWriter.ToString();
        }

        private static void Deserialize()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(PropfindRes), "D");
            
            var result= xmlSerializer.Deserialize(new StringReader(Res1.PropfindRes));
            Serialize<PropfindRes>((PropfindRes)result);
        }

        private static void Serialize<T>(T info)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("D", "DAV:");
            XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            StringWriter textWriter = new StringWriter();
            xmlSerializer.Serialize(textWriter, info, ns);
            var result = textWriter.ToString();
        }


        private static void StartSers()
        {
            log.Info("Start");
            //http://localhost:7011/webdav/doc1.docx
            //http://localhost:7011/webdav/doc1.docx
            //http://192.168.145.128:7011/webdav/doc1.docx
            //http://1192.168.36.130:7011/webdav/doc1.docx

            string baseAddress = "http://*:7011/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.ReadLine();
            }
        }
    }
}
