using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Server.HotSpringRooms
{
   public class HotSpringProcessorAttribute : Attribute
    {
        private byte _code;
        private string _descript;

        public HotSpringProcessorAttribute(byte code, string description)
        {
            this._code = code;
            this._descript = description;
        }

        public byte Code
        {
            get
            {
                return this._code;
            }
        }

        public string Description
        {
            get
            {
                return this._descript;
            }
        }
    }
}

