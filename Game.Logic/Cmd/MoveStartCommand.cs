using Game.Base.Packets;
using Game.Logic.Phy.Object;
using System;

namespace Game.Logic.Cmd
{
    [GameCommand((byte)eTankCmdType.MOVESTART, "开始移动")]
    public class MoveStartCommand : ICommandHandler
    {
        public void HandleCommand(BaseGame game, Player player, GSPacketIn packet)
        {
            if (player.IsAttacking)
            {
                //GSPacketIn pkg = packet.Clone();
                //pkg.ClientID = player.PlayerDetail.PlayerCharacter.ID;
                //pkg.Parameter1 = player.Id;
                //game.SendToAll(pkg, player.PlayerDetail);

                byte type = packet.ReadByte();
                int tx = packet.ReadInt();
                int ty = packet.ReadInt();
                byte dir = packet.ReadByte();
                bool isLiving = packet.ReadBoolean();
                //_loc_7.writeShort(param6); _player.map.currentTurn
                short map_currentTurn = packet.ReadShort();
                game.SendPlayerMove(player, type, tx, ty, dir, isLiving);               
                switch (type)
                {
                    case 0:
                    case 1:
                        
                        player.SetXY(tx, ty);                       
                        player.StartMoving();
                        if (player.Y - ty > 1 || player.IsLiving != isLiving)
                        {
                            //trminhpc type=3
                            game.SendPlayerMove(player, 3, player.X, player.Y, 0);//, player.IsLiving, null);
                        }
                        //else
                        //{
                        //    game.SendPlayerMove(player, type, tx, ty, dir, isLiving, null);
                        //}
                        break;
                    
                }
            }
        }
    }
}
