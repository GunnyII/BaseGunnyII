using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDataProvider.Data;
using System.Data.SqlClient;

namespace Bussiness
{
    public class PveBussiness : BaseBussiness
    {
        public PveInfo[] GetAllPveInfos()
        {
            List<PveInfo> infos = new List<PveInfo>();
            SqlDataReader reader = null;
            try
            {
                db.GetReader(ref reader, "SP_PveInfos_All");
                while (reader.Read())
                {
                    PveInfo info = new PveInfo
                    {
                        ID = (int)reader["Id"],
                        Name = (reader["Name"] == null) ? "" : reader["Name"].ToString(),
                        Type = (int)reader["Type"],
                        LevelLimits = (int)reader["LevelLimits"],
                        SimpleTemplateIds = (reader["SimpleTemplateIds"] == null) ? "" : reader["SimpleTemplateIds"].ToString(),
                        NormalTemplateIds = (reader["NormalTemplateIds"] == null) ? "" : reader["NormalTemplateIds"].ToString(),
                        HardTemplateIds = (reader["HardTemplateIds"] == null) ? "" : reader["HardTemplateIds"].ToString(),
                        TerrorTemplateIds = (reader["TerrorTemplateIds"] == null) ? "" : reader["TerrorTemplateIds"].ToString(),
                        Pic = (reader["Pic"] == null) ? "" : reader["Pic"].ToString(),
                        Description = (reader["Description"] == null) ? "" : reader["Description"].ToString(),
                        Ordering = (int)reader["Ordering"],
                        AdviceTips = (reader["AdviceTips"] == null) ? "" : reader["AdviceTips"].ToString(),
                        SimpleGameScript = reader["SimpleGameScript"] as string,
                        NormalGameScript = reader["NormalGameScript"] as string,
                        HardGameScript = reader["HardGameScript"] as string,
                        TerrorGameScript = reader["TerrorGameScript"] as string
                    };

                    infos.Add(info);
                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("GetAllMap", e);
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }
            return infos.ToArray();
        }
    }
}
