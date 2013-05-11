using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.Managers;
using Game.Server.GameObjects;
using Game.Server.GameUtils;
using Game.Server.Rooms;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.GOODS_COUNT, "场景用户离开")]
    public class UpdateGoodsCountHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            int consortia_id = packet.ReadInt();
            /*
            var _loc_4:* = _loc_2.readInt();
            var _loc_5:int = 0;
            while (_loc_5 < _loc_4)
            {
                
                _loc_10 = _loc_2.readInt(); //GoodsID
                _loc_11 = _loc_2.readInt(); //LimitCount 
                _loc_12 = getShopItemByGoodsID(_loc_10);
                if (_loc_12)
                {
                }
                if (_loc_3 == 1)
                {
                    _loc_12.LimitCount = _loc_11;
                }
                _loc_5 = _loc_5 + 1;
            }
            var _loc_6:* = _loc_2.readInt(); //ConsortiaID
            var _loc_7:* = _loc_2.readInt();
            var _loc_8:int = 0;
            while (_loc_8 < _loc_7)
            {
                
                _loc_13 = _loc_2.readInt(); //GoodsID
                _loc_14 = _loc_2.readInt(); //LimitCount
                _loc_15 = getShopItemByGoodsID(_loc_13);
                if (_loc_15)
                {
                }
                if (_loc_3 == 2)
                {
                }
                if (_loc_6 == PlayerManager.Instance.Self.ConsortiaID)
                {
                    _loc_15.LimitCount = _loc_14;
                }
                _loc_8 = _loc_8 + 1;
            }
            var _loc_9:* = _loc_2.readInt();
             */
            return 0;
        }
    }
}
