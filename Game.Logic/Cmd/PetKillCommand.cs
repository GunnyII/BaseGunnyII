using Game.Base.Packets;
using Game.Logic.Phy.Object;
using SqlDataProvider.Data;
using Bussiness.Managers;

namespace Game.Logic.Cmd
{
    /// <summary>
    /// 使用道具协议
    /// </summary>
    [GameCommand((byte)eTankCmdType.PET_SKILL, "使用道具")]
    public class PetKillCommand : ICommandHandler
    {
        public void HandleCommand(BaseGame game, Player player, GSPacketIn packet)
        {
            if (game.GameState != eGameState.Playing || player.GetSealState())
                return;           
            
            int killID = packet.ReadInt();
            //game.SendPetUseKill(player, killID, true);
            player.PetUseKill(killID);
        }
    }
}
