using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace webdavSrv
{
    public static class Helper
    {
        public static string Serialize2<T>(T info)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("D", "DAV:");
            XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            
            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Indent = true;
            //settings.Encoding = Encoding.UTF8;
            StringWriter textWriter = new StringWriter();
            XmlWriter writer = XmlWriter.Create(textWriter);
            xmlSerializer.Serialize(writer, info, ns);
            return textWriter.ToString().Replace("utf-16", "utf-8");

        }

        public static string Serialize<T>(T info)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("D", "DAV:");
            XmlSerializer xmlSerializer = new XmlSerializer(info.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlWriter xmlwriter = new XmlTextWriter(ms, Encoding.UTF8))
                {
                    xmlSerializer.Serialize(xmlwriter, info, ns);
                    ms.Position = 0;
                    byte[] msBytes = new byte[ms.Length];
                    ms.Read(msBytes, 0, (int)ms.Length);
                    var data = Encoding.UTF8.GetString(msBytes);
                    var result = data.Substring(1, data.Length - 1);
                    return result;
                }
            }
        }

        public static T Deserialize<T>(string xml)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), "D");

            var result = xmlSerializer.Deserialize(new StringReader(xml));
            return (T)result;
        }

        public static bool FileOrDirectoryExists(string name)
        {
            return (Directory.Exists(name) || File.Exists(name));
        }

        public static response GetResponse(string resName, string basePath, string baseUrl)
        {
            string fullName = basePath + "\\" + resName;
            FileAttributes attr = File.GetAttributes(fullName);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return GetDirResponse(resName, basePath, baseUrl);
            }
            else
            {
                return GetFileResponse(resName, basePath, baseUrl);
            }
        }

        public static response GetFileResponse(string resName, string basePath, string baseUrl)
        {
            string fullName = basePath + "\\" + resName;
            var fileInfo = new System.IO.FileInfo(fullName);
            response fileRes = new response() { href = baseUrl + "/" + resName };
            fileRes.propstat = new propstat() { status = "HTTP/1.1 200 OK" };
            fileRes.propstat.prop = new Prop()
            {
                lockdiscovery = new lockdiscovery(),
                ishidden = fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "1" : "0",
                lastaccessed = fileInfo.LastAccessTime.ToString("ddd, dd MMM yyyy HH:mm:ss GMT"),
                creationdate = fileInfo.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                displayname = fileInfo.Name,
                getcontentlanguage = ""
            };
            fileRes.propstat.prop.supportedlock = new supportedlock();
            fileRes.propstat.prop.supportedlock.lockentry = new List<lockentry>();
            fileRes.propstat.prop.supportedlock.lockentry.Add(new lockentry() { lockscope = new lockscope() { exclusive = new object() }, locktype = new locktype() { write = new object() } });
            fileRes.propstat.prop.supportedlock.lockentry.Add(new lockentry() { lockscope = new lockscope() { shared = new object() }, locktype = new locktype() { write = new object() } });

            fileRes.propstat.prop.lockdiscovery = new lockdiscovery();
            fileRes.propstat.prop.getcontenttype = System.Web.MimeMapping.GetMimeMapping(fullName);
            fileRes.propstat.prop.getcontentlength = fileInfo.Length.ToString();
            fileRes.propstat.prop.getetag = "\"d1a1f59cdadd71:0\"";
            fileRes.propstat.prop.iscollection = "0";

            fileRes.propstat.prop.getcontentlength = fileInfo.Length.ToString();
            fileRes.propstat.prop.getlastmodified = fileInfo.LastWriteTime.ToString("ddd, dd MMM yyyy HH:mm:ss GMT");

            return fileRes;
        }

        public static response GetDirResponse(string resName, string basePath, string baseUrl)
        {
            response dirRes = new response() { href = baseUrl + (resName == "/" ? resName : "/" + resName + "/") };
            dirRes.propstat = new propstat();
            string fullName = basePath + "\\" + resName;
            var dirInfo = new System.IO.DirectoryInfo(fullName);
            dirRes.propstat.status = "HTTP/1.1 200 OK";
            dirRes.propstat.prop = new Prop()
            {
                iscollection = "1",
                ishidden = dirInfo.Attributes.HasFlag(FileAttributes.Hidden) ? "1" : "0",
                creationdate = dirInfo.CreationTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                getlastmodified = dirInfo.LastWriteTime.ToString("ddd, dd MMM yyyy HH:mm:ss GMT"),

            };
            dirRes.propstat.prop.lockdiscovery = new lockdiscovery();
            dirRes.propstat.prop.getcontenttype = "";
            dirRes.propstat.prop.getcontentlength = "0";
            dirRes.propstat.prop.getetag = "";
            dirRes.propstat.prop.displayname = ((resName == "/" ? "webdav" : dirInfo.Name));
            dirRes.propstat.prop.getcontentlanguage = "";
            dirRes.propstat.prop.resourcetype = new resourcetype() { collection = new object() };
            dirRes.propstat.prop.supportedlock = new supportedlock();
            dirRes.propstat.prop.supportedlock.lockentry = new List<lockentry>();
            dirRes.propstat.prop.supportedlock.lockentry.Add(new lockentry() { lockscope = new lockscope() { exclusive = new object() }, locktype = new locktype() { write = new object() } });
            dirRes.propstat.prop.supportedlock.lockentry.Add(new lockentry() { lockscope = new lockscope() { shared = new object() }, locktype = new locktype() { write = new object() } });
            return dirRes;
        }
    }
}