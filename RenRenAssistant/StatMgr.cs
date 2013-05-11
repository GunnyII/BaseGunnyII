using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

namespace RenRenAssistant
{
    public class StatMgr
    {
        public static string LogPath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogPath"];
            }
        }

        public static bool SaveError(string billId,string uid,int amount,string error)
        {
            try
            {
                string CurrentPath = HttpContext.Current.Server.MapPath("~");
                string _record = CurrentPath + LogPath;

                if (!Directory.Exists(_record))
                {
                    Directory.CreateDirectory(_record);
                }

                string file = string.Format("{0}\\{1:yyyyMMdd}.log", _record, DateTime.Now);
                using (FileStream fs = File.Open(file, FileMode.Append))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine(string.Format("{0},{1},{2},{3},{4}", billId, DateTime.Now, uid, amount, error));
                    }
                }

            }
            catch
            {
                return false;
            }
            return true;
        } 
    }
}
