using System;
using System.Collections.Generic;
using log4net;
using System.Reflection;
using System.Threading;
using Bussiness;
using SqlDataProvider.Data;
using System.IO;
using Game.Logic.Phy.Maps;
using Game.Logic.Phy.Object;

namespace Game.Logic
{
    public class VaneMgr
    {
        /// <summary>
        /// Defines a logger for this class.
        /// </summary>
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<byte[], VaneInfo> m_infos;

        public static bool ByteAray()
        {
            return ReLoad();
        }

        public static bool ReLoad()
        {
            try
            {
                Dictionary<byte[], VaneInfo> tempVanes = LoadFromDatabase();
                if (tempVanes.Values.Count > 0)
                {
                    Interlocked.Exchange(ref m_infos, tempVanes);
                    return true;
                }
            }
            catch (Exception ex)
            {
                log.Error("Vane Mgr byte[] error:", ex);
            }
            return false;
        }

        private static Dictionary<byte[], VaneInfo> LoadFromDatabase()
        {
            Dictionary<byte[], VaneInfo> list = new Dictionary<byte[], VaneInfo>();
            using (ProduceBussiness db = new ProduceBussiness())
            {
                VaneInfo[] vaneInfos = db.GetAllVane();
                foreach (VaneInfo b in vaneInfos)
                {
                    if (!list.ContainsKey(b.Param1))
                    {
                        list.Add(b.Param1, b);
                    }
                }
            }
            return list;
        }
         

    }
}
