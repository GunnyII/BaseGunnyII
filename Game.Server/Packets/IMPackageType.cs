using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Server.Packets
{
    public enum IMPackageType
    {
        FRIEND_ADD = 160,
        FRIEND_REMOVE = 161,
        FRIEND_UPDATE = 162,
        FRIEND_STATE = 165,
        ONS_EQUIP = 45,
        FRIEND_RESPONSE = 166,
        SAME_CITY_FRIEND = 164,
        ADD_CUSTOM_FRIENDS = 208,
        ONE_ON_ONE_TALK = 51,
    }
}
