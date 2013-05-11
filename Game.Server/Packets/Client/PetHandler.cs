using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using SqlDataProvider.Data;
using Bussiness;

namespace Game.Server.Packets.Client
{
    [PacketHandler((byte)ePackageType.PET, "添加好友")]
    public class PetHandler : IPacketHandler
    {
        //0友好，1黑名单
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            var pet_cmd = packet.ReadByte();
            switch (pet_cmd)
            {
                case (byte)Game.Logic.eTankCmdType.UPDATE_PET:
                    HandleUPDATE_PET(client, packet.ReadInt());
                    break;
                case (byte)Game.Logic.eTankCmdType.ADD_PET://ADD_PET
                    break;
                case (byte)Game.Logic.eTankCmdType.MOVE_PETBAG://MOVE_PETBAG
                    break;
                case (byte)Game.Logic.eTankCmdType.FEED_PET://FEED_PET
                    break;
                case (byte)Game.Logic.eTankCmdType.REFRESH_PET://REFRESH_PET
                    client.Out.SendRefreshPet();
                    break;
                case (byte)Game.Logic.eTankCmdType.ADOPT_PET://ADOPT_PET
                    break;
                case (byte)Game.Logic.eTankCmdType.EQUIP_PET_SKILL://EQUIP_PET_SKILL
                    break;
                case (byte)Game.Logic.eTankCmdType.RELEASE_PET://RELEASE_PET
                    break;
                case (byte)Game.Logic.eTankCmdType.RENAME_PET://RENAME_PET
                    break;
                case (byte)Game.Logic.eTankCmdType.PAY_SKILL://PAY_SKILL
                    break;
                case (byte)Game.Logic.eTankCmdType.FIGHT_PET://FIGHT_PET
                    break;
            }
            return 0;
        }
        protected void HandleUPDATE_PET(GameClient client, int ID)
        {            
            GamePlayer player = Managers.WorldMgr.GetPlayerById(ID);
            PlayerInfo info;
            List<ItemInfo> items;
            if (player != null)
            {
                info = player.PlayerCharacter;
                items = player.CardBag.GetItems(0, 6);
            }
            else
            {
                using (PlayerBussiness pb = new PlayerBussiness())
                {
                    info = pb.GetUserSingleByUserID(ID);
                    items = pb.GetUserCardEuqip(ID);
                }
            }

            if (info != null && items != null)
                client.Out.SendUpdatePetInfo(info, items);
           
        }
    }
}
