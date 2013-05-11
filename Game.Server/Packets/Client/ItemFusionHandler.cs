using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Bussiness;
using Bussiness.Managers;
using SqlDataProvider.Data;
using Game.Server.Managers;
using Game.Server.GameUtils;
using System.Configuration;
using Game.Server.Statics;


namespace Game.Server.Packets.Client
{
    
    [PacketHandler((byte)ePackageType.ITEM_FUSION, "熔化")]
    public class ItemFusionHandler : IPacketHandler
    {
        /// <summary>
        /// 
        /// </summary>
        public static List<int> FusionFormulID = new List<int> { 11201, 11202, 11203, 11204, 11301, 11302, 11303, 11304 };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="packet"></param>
        /// <returns></returns>
        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("Chức năng tạm khóa!"));
            return 0;
        }
    
    }
}
