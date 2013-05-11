using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;

namespace Game.Server.Rooms
{
    public class ExitRoomAction:IAction
    {
        BaseRoom m_room;
        GamePlayer m_player;

        public ExitRoomAction(BaseRoom room,GamePlayer player)
        {
            m_room = room;
            m_player = player;
        }

        public void Execute()
        {
            m_room.RemovePlayerUnsafe(m_player);
            //---------------
            BaseRoom[] list = RoomMgr.Rooms;
            List<BaseRoom> tempList = new List<BaseRoom>();
           
            for (int i = 0; i < list.Length; i++)
            {
                if (!list[i].IsEmpty)
                {
                    tempList.Add(list[i]);
                   
                }
            }
            /////
            m_player.Out.SendUpdateRoomList(tempList);
            GSPacketIn pkg = m_player.Out.SendUpdateRoomList(tempList); //m_player.Out.SendUpdateRoomList(m_room);
            RoomMgr.WaitingRoom.SendToALL(pkg, m_player);
            if (m_room.PlayerCount == 0)
            {
                m_room.Stop();
            }
        }
    }
}
