using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Server.GameObjects;
using Game.Base.Packets;
using Game.Logic;
using Game.Server.Games;
using Game.Server.Battle;
using Game.Server.Packets;
using Game.Logic.Phy.Object;

namespace Game.Server.Rooms
{
    public class StartGameMissionAction : IAction
    {
        BaseRoom m_room;

        public StartGameMissionAction(BaseRoom room)
        {
            m_room = room;
        }

        public void Execute()
        {
            
            foreach (Player p in m_room.BaseGames.Players.Values)
            {
                p.Ready = true;                
            }
            // m_room.BaseGames.Players[0].Ready = true;
            //m_room.BaseGames.SendSyncLifeTime();
            m_room.BaseGames.CheckState(0);           
            m_room.Host.Out.SendGameMissionStart();
           
        }
       
    }
}
