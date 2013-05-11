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
    [PacketHandler((int)ePackageType.GAME_ROOM, "游戏创建")]
    public class GameRoomHandler : IPacketHandler
    {      
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            var game_room = packet.ReadInt();
            
            switch (game_room)
            {
                case (int)GameRoomPackageType.GAME_ROOM_SETUP_CHANGE:
                    {
                        if (client.Player.CurrentRoom != null && client.Player == client.Player.CurrentRoom.Host && !client.Player.CurrentRoom.IsPlaying)
                        {
                            int mapId = packet.ReadInt();
                                if (mapId == 10000) return 0;
                            eRoomType roomType = (eRoomType)packet.ReadByte();
                            bool isOpenBoss = packet.ReadBoolean();//_loc_12.isOpenBoss.writeBoolean(param3);
                            string roomPass = packet.ReadString();//_current.roomPass = event.pkg.readUTF();
                            string roomName = packet.ReadString();//_current.roomName = event.pkg.readUTF();            
                            byte timeType = packet.ReadByte();
                            byte hardLevel = packet.ReadByte();
                            int levelLimits = packet.ReadInt();
                            bool isCrossZone = packet.ReadBoolean();//_current.isCrossZone = event.pkg.readBoolean();
                            int mapId2 = packet.ReadInt();
                            //client.Player.SendMessage("MapID: " + mapId.ToString() + "|" + roomType.ToString());
                            RoomMgr.UpdateRoomGameType(client.Player.CurrentRoom,
                                roomType, timeType, (eHardLevel)hardLevel, levelLimits, 
                                mapId, roomPass, roomName, isCrossZone, isOpenBoss);                            
                        }                        
                        break;
                    }
                case (int)GameRoomPackageType.GAME_ROOM_REMOVEPLAYER:
                    {
                        if (client.Player.CurrentRoom != null)
                        {
                            RoomMgr.ExitRoom(client.Player.CurrentRoom, client.Player);
                        }
                        //RoomMgr.ExitWaitingRoom(client.Player);                        
                        break;
                    }
                case (int)GameRoomPackageType.GAME_ROOM_UPDATE_PLACE:
                    {
                        if (client.Player.CurrentRoom != null && client.Player == client.Player.CurrentRoom.Host)
                        {
                            byte playerPlace = packet.ReadByte();
                            int place = packet.ReadInt();
                            bool isOpen = packet.ReadBoolean();
                            int placeView = packet.ReadInt();
                            //client.Player.SendMessage("GAME_ROOM_UPDATE_PLACE: " +
                            //    playerPlace.ToString() + "|" + place.ToString() + "|" +
                            //    isOpen.ToString() + "|" +
                            //    placeView.ToString());
                            RoomMgr.UpdateRoomPos(client.Player.CurrentRoom, playerPlace, isOpen, place, placeView);
                        }
                        break;
                    }
                    
                case (int)GameRoomPackageType.GAME_PICKUP_CANCEL:
                    {
                        // GAME_AWIT_CANCEL
                        RoomMgr.CancelPickup(client.Player.CurrentRoom.BattleServer, client.Player.CurrentRoom);                        
                        //GSPacketIn pkg = client.Player.Out.SendRoomPairUpCancel(client.Player.CurrentRoom);
                        //client.Player.CurrentRoom.SendToAll(pkg, client.Player);
                        break;
                    }
                case (int)GameRoomPackageType.GAME_PICKUP_STYLE:
                    {                       
                        //_loc_2.writeInt(param1); 2 opton sendGameStyle and sendGameMode
                        //GMAE_STYLE_RECV FREE_MODE:int = 0; GUILD_MODE:int = 1;
                        int game = packet.ReadInt();
                        if (client.Player.CurrentRoom != null)
                        {
                            int GameStyle = packet.ReadInt();
                            switch (GameStyle)
                            {
                                case 0:
                                    client.Player.CurrentRoom.GameType = eGameType.Free;
                                    break;
                                default:
                                    client.Player.CurrentRoom.GameType = eGameType.Guild;
                                    break;
                            }
                            GSPacketIn pkg = client.Player.Out.SendRoomType(client.Player, client.Player.CurrentRoom);
                            client.Player.CurrentRoom.SendToAll(pkg, client.Player);
                        }
                      
                        break;
                    }                
                
                case (int)GameRoomPackageType.GAME_PLAYER_STATE_CHANGE:
                    {
                        if (client.Player.MainWeapon == null)
                        {
                            client.Player.SendMessage(LanguageMgr.GetTranslation("Game.Server.SceneGames.NoEquip"));
                            return 0;
                        }
                        if (client.Player.CurrentRoom != null)
                        {
                            RoomMgr.UpdatePlayerState(client.Player, packet.ReadByte());
                        }
                        break;
                    }
                
                case (int)GameRoomPackageType.GAME_ROOM_CREATE:
                    {
                        byte roomType = packet.ReadByte();
                        byte timeType = packet.ReadByte();
                        string roomName = packet.ReadString();
                        string pwd = packet.ReadString();

                        RoomMgr.CreateRoom(client.Player, roomName, pwd, (eRoomType)roomType, timeType);
                        break;
                    }
                case (int)GameRoomPackageType.GAME_ROOM_KICK:
                    {
                        //_loc_2.writeByte(param1);
                        if (client.Player.CurrentRoom != null && client.Player == client.Player.CurrentRoom.Host)
                        {
                            RoomMgr.KickPlayer(client.Player.CurrentRoom, packet.ReadByte());
                        }
                        break;
                    }
                case (int)GameRoomPackageType.GAME_ROOM_LOGIN:
                    {
                        bool isInvite = packet.ReadBoolean();
                        int type = packet.ReadInt();
                        int isRoundID = packet.ReadInt();
                        int roomId = -1;
                        string pwd = null;

                        if (isRoundID == -1)
                        {
                            roomId = packet.ReadInt();
                            pwd = packet.ReadString();
                        }
                        if (type == 1) type = 0;
                        else if (type == 2) type = 4;

                        RoomMgr.EnterRoom(client.Player, roomId, pwd, type);

                        break;
                    }
                case (int)GameRoomPackageType.GAME_START:
                    {                        
                        BaseRoom room = client.Player.CurrentRoom;
                        if (room != null && room.Host == client.Player)
                        {
                            if (client.Player.MainWeapon == null)
                            {
                                client.Player.SendMessage(LanguageMgr.GetTranslation("Game.Server.SceneGames.NoEquip"));
                                return 0;
                            }

                            if (room.RoomType == eRoomType.Dungeon)
                            {
                                if (!client.Player.IsPvePermission(room.MapId, room.HardLevel))
                                {
                                    //TODO 写入语言包中，以便多语言转换
                                    client.Player.SendMessage("Do not PvePermission enter this map!");
                                    return 0;
                                }
                            }
                           
                            RoomMgr.StartGame(client.Player.CurrentRoom);
                           
                        }
                        break;
                    }
                case (int)GameRoomPackageType.GAME_TEAM:
                    {
                        //_loc_2.writeByte(param1);
                        if (client.Player.CurrentRoom == null || client.Player.CurrentRoom.RoomType == eRoomType.Match)
                            return 0;

                        RoomMgr.SwitchTeam(client.Player);
                        break;
                    }
               
                case (int)GameRoomPackageType.ROOMLIST_UPDATE:
                    {
                        int hallType = packet.ReadInt();
                        int updateType = packet.ReadInt();
                        int pveMapRoom = 10000;
                        int pveHardLeve = 1011;
                        if (updateType == -2)
                        {
                            pveMapRoom = packet.ReadInt();
                            pveHardLeve = packet.ReadInt();
                        }
                        BaseRoom[] list = RoomMgr.Rooms;
                        List<BaseRoom> tempList = new List<BaseRoom>();

                        for (int i = 0; i < list.Length; i++)
                        {
                            if (!list[i].IsEmpty)
                            {
                                tempList.Add(list[i]);

                            }
                        }
                        if (tempList.Count > 0) client.Out.SendUpdateRoomList(tempList);
                        break;
                    }
            }

            return 0;
        }
    }
}
