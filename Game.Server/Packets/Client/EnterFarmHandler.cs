using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Game.Server.GameObjects;
using Game.Server.Managers;
using Bussiness;
using Game.Server.Rooms;
using Game.Logic;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.FARM, "游戏创建")]
    public class EnterFarmHandler : IPacketHandler
    {        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            byte game_room = packet.ReadByte();           
            switch (game_room)
            {
                case (int)FarmPackageType.ACCELERATE_FIELD:
                    {

                        break;
                    }
                case (int)FarmPackageType.COMPOSE_FOOD:
                    {

                        break;
                    }
                case (int)FarmPackageType.ENTER_FARM:
                    {
                        int userId = packet.ReadInt();
                        client.Out.SendEnterFarm(client.Player);                        
                        break;
                    }

                case (int)FarmPackageType.EXIT_FARM:
                    {

                        break;
                    }
                case (int)FarmPackageType.FRUSH_FIELD:
                    {

                        break;
                    }

                case (int)FarmPackageType.GAIN_FIELD:
                    {

                        break;
                    }

                case (int)FarmPackageType.GROW_FIELD:
                    {
                        break;
                    }
                case (int)FarmPackageType.HELPER_PAY_FIELD:
                    {

                        break;
                    }
                case (int)FarmPackageType.HELPER_SWITCH_FIELD:
                    {

                        break;
                    }
                case (int)FarmPackageType.KILLCROP_FIELD:
                    {

                        break;
                    }
                case (int)FarmPackageType.PAY_FIELD:
                    {

                        break;
                    }


            }

            return 0;
        }
    }
}
