using Game.Base.Packets;
using Game.Logic.Phy.Object;
using SqlDataProvider.Data;
using Bussiness.Managers;

namespace Game.Logic.Cmd
{
   //[GameCommand((byte)eTankCmdType.PROP_DELETE, "使用道具")]
    public class PropDeleteCommand : ICommandHandler
    {
       public void HandleCommand(BaseGame game, Player player, GSPacketIn packet)
        {

            int index = packet.ReadInt();
            //player.PlayerDetail.DeletePropItem(index);
            
        }
    }
}
