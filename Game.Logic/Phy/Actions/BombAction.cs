using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Logic.Phy.Actions
{
    public class BombAction
    {
        public float Time;

        public int Type;

        public int Param1;

        public int Param2;

        public int Param3;

        public int Param4;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time"></param>
        /// <param name="type"></param>
        /// <param name="para1">id</param>
        /// <param name="para2">damage + critical</param>
        /// <param name="para3">critical</param>
        /// <param name="para4">blood</param>
        public BombAction(float time, ActionType type, int para1,int para2,int para3,int para4)
        {
            Time = time;
            Type = (int)type;
            Param1 = para1;//id
            Param2 = para2;//damage + critical
            Param3 = para3;//critical
            Param4 = para4;//blood
        }

        public int TimeInt
        {
            get
            {
                return (int)Math.Round(Time * 1000);
            }
        }
    }
}
