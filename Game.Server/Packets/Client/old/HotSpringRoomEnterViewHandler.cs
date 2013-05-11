namespace Game.Server.Packets.Client
{
    using Game.Base.Packets;
    using Game.Server;
    using SqlDataProvider.Data;
    using System;

    [PacketHandler(0xc9, "礼堂数据")]
    public class HotSpringRoomEnterViewHandler : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            GSPacketIn pkg = new GSPacketIn(0xc6);
            PlayerInfo playerCharacter = client.Player.PlayerCharacter;
            pkg.WriteInt(playerCharacter.ID);
            pkg.WriteInt(playerCharacter.Grade);
            pkg.WriteInt(playerCharacter.Hide);
            pkg.WriteInt(playerCharacter.Repute);
            pkg.WriteString(playerCharacter.NickName);
            pkg.WriteBoolean(true);
            pkg.WriteInt(5);
            pkg.WriteBoolean(playerCharacter.Sex);
            pkg.WriteString(playerCharacter.Style);
            pkg.WriteString(playerCharacter.Colors);
            pkg.WriteString(playerCharacter.Skin);
            pkg.WriteInt(300);
            pkg.WriteInt(400);
            pkg.WriteInt(playerCharacter.FightPower);
            pkg.WriteInt(playerCharacter.Win);
            pkg.WriteInt(playerCharacter.Total);
            pkg.WriteInt(1);
            client.SendTCP(pkg);
            return 0;
        }
    }
}

