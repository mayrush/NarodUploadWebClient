using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace NarodUploadWebClient
{
    class XmlFile
    {
        public string Filename = "files.xml";
        public XElement XmlData;
        public string RootName = "Root";

        public void OpenXmlFile()
        {
            if (!File.Exists(Filename))
            {
                XmlData = new XElement(RootName);
                XmlData.Save(Filename);
            }
            else
            {
                XmlData = XElement.Load(Filename);
            }
        }

        public void RemoveNotRefreshed()
        {
            if (XmlData == null) OpenXmlFile();

            var v = XmlData.Elements("file").Where(page => "0" == page.Element("ref").Value);

            if (v.Count() != 0)
            {
                v.Remove();
            }
            XmlData.Save(Filename);
        }

        public void SetRefreshedToZero()
        {
            if (XmlData == null) OpenXmlFile();

            var v = from page in XmlData.Elements("file")
                    select page;

            if (v.Count() != 0)
            {
                foreach (var element in v)
                {
                    if (element != null) element.Element("ref").Value = "0";
                }
            }
        }

        public void ChangeFolder(string[] fileids, string folder)
        {
            if (XmlData == null) OpenXmlFile();

            foreach (var fileid in fileids)
            {
                var v = XmlData.Elements("file").Where(page => fileid == page.Element("fid").Value);

                if (v.Count() != 0)
                {
                    foreach (var element in v)
                    {
                        element.Element("folder").Value = folder;
                    }
                }
            }
            XmlData.Save(Filename);
        }

        public string GetToken(string fileid)
        {
            if (XmlData == null) OpenXmlFile();
            var v = XmlData.Elements("file").Where(page => fileid == page.Element("fid").Value);
            if (v.Count() != 0)
            {
                foreach (var element in v)
                {
                    return element.Element("token").Value;
                }
            }
            return "0000000000000000";
        }

        public string GetLink(string fileid)
        {
            if (XmlData == null) OpenXmlFile();
            var v = XmlData.Elements("file").Where(page => fileid == page.Element("fid").Value);
            if (v.Count() != 0)
            {
                foreach (var element in v)
                {
                    return element.Element("url").Value;
                }
            }
            return "narod.ru";
        }

        public void AddFile(string[] files)
        {
            if (XmlData == null) OpenXmlFile();

            var v = XmlData.Elements("file").Where(page => files[1] == page.Element("fid").Value);

            if (v.Count() == 0)
            {
                var newXmlElement = new XElement("file",
                                                 new XElement("fid", files[1]),
                                                 new XElement("icon", files[0]),
                                                 new XElement("name", files[3]),
                                                 new XElement("url", files[2]),
                                                 new XElement("date", files[4]),
                                                 new XElement("ref", "1"),
                                                 new XElement("tag", "null"),
                                                 new XElement("folder", "root"),
                                                 new XElement("token", files[5]),
                                                 new XElement("size", files[6]),
                                                 new XElement("stat", files[7]),
                                                 new XElement("uploaddate", files[8])
                    );
                XmlData.Add(newXmlElement);
            }
            else
            {
                var xmlElement = v.First();
                xmlElement.Element("date").Value = files[4];
                xmlElement.Element("ref").Value = "1";
                xmlElement.Element("token").Value = files[5];
                xmlElement.Element("stat").Value = files[7];
                xmlElement.Element("uploaddate").Value = files[8];
            }
            if (XmlData != null)
            {
                XmlData.Save(Filename);
            }
        }

        public void DeleteFile(string[] fileids)
        {
            if (XmlData == null) OpenXmlFile();

            foreach (var v in
                fileids.Select(fileid => XmlData.Elements("file").Where(page => fileid == page.Element("fid").Value)))
            {
                v.Remove();
            }
            XmlData.Save(Filename);
        }
    }
}