using Game.Base.Packets;
using Game.Logic.Phy.Object;
using SqlDataProvider.Data;
using Bussiness.Managers;

namespace Game.Logic.Cmd
{
    /// <summary>
    /// 使用道具协议
    /// </summary>
    //[GameCommand((byte)eTankCmdType.PET_BEAT, "使用道具")]
    public class PetBeatCommand : ICommandHandler
    {
        public void HandleCommand(BaseGame game, Player player, GSPacketIn packet)
        {
            if (game.GameState != eGameState.Playing || player.GetSealState())
                return;

            int type = packet.ReadByte();
            int place = packet.ReadInt();
            int templateID = packet.ReadInt();           
            ItemTemplateInfo template = ItemMgr.FindItemTemplate(templateID);            
            if (player.CanUseItem(template))
            {                
                if (player.PlayerDetail.UsePropItem(game, type, place, templateID, player.IsLiving))
                {                    
                    if (player.UseItem(template) == false)
                    {
                        BaseGame.log.Error("Using prop error");
                    }
                }
                else
                {
                    player.UseItem(template);
                    //player.Prop = template;
                }
            }
        }
    }
}
