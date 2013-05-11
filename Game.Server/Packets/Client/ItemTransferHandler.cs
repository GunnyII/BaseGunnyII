using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Bussiness;
using SqlDataProvider.Data;
using System.Configuration;
using Game.Server.Managers;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.ITEM_TRANSFER, "物品转移")]
    public class ItemTransferHandler : IPacketHandler
    {
        public void GetWeaponID(ref int id0, ref int id1)
        {
            //int _idItem0 = id0;
            //int _idItem1 = id1;
            string _id0 = id0.ToString().Substring(0, 4); //70081
            string _id1 = id1.ToString().Substring(0, 4);
            //string _id0Tail = id0.ToString().Substring(4);
            _id0 += id1.ToString().Substring(4);
            //string _id1Tail = id1.ToString().Substring(4);
            _id1 += id0.ToString().Substring(4);
            //if (_id0 == "" && _id1 == "")
            //{
            id0 = int.Parse(_id0);
            id1 = int.Parse(_id1);
            //}
        }
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            GSPacketIn pkg = packet.Clone();
            pkg.ClearContext();

            StringBuilder str = new StringBuilder();
            int mustGold = 40000;            
            bool _moveHole = packet.ReadBoolean();
            bool _moveFivSixHole = packet.ReadBoolean();

            ItemInfo transferBefore = client.Player.StoreBag2.GetItemAt(0);
            ItemInfo transferAfter = client.Player.StoreBag2.GetItemAt(1);           
            //未开始
            if (transferBefore != null && transferAfter != null && 
                transferBefore.Template.CategoryID == transferAfter.Template.CategoryID && 
                //transferBefore.Template.CategoryID < 10 &&
                transferAfter.Count == 1 && transferBefore.Count == 1 && 
                transferBefore.IsValidItem() && transferAfter.IsValidItem())
            {
               
                if (client.Player.PlayerCharacter.Gold < mustGold)
                {
                    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("itemtransferhandler.nogold"));
                    return 1;
                }
                    client.Player.RemoveGold(mustGold);

                    if (transferBefore.Template.CategoryID == 7 || transferAfter.Template.CategoryID == 7)
                    {
                        ItemTemplateInfo newTemp0 = null;
                        ItemTemplateInfo newTemp1 = null;
                        ItemInfo newItem0 = null;
                        ItemInfo newItem1 = null;
                        int _temID0 = transferBefore.TemplateID;
                        int _temID1 = transferAfter.TemplateID;

                        GetWeaponID(ref _temID0, ref _temID1);

                        newTemp0 = Bussiness.Managers.ItemMgr.FindItemTemplate(_temID0);
                        newTemp1 = Bussiness.Managers.ItemMgr.FindItemTemplate(_temID1);
                        if (newTemp0 != null)
                        newItem0 = ItemInfo.CreateWeapon(newTemp0, transferBefore, (int)ItemAddType.Strengthen);
                        transferBefore = newItem0;
                        if (newTemp1 != null)
                        newItem1 = ItemInfo.CreateWeapon(newTemp1, transferAfter, (int)ItemAddType.Strengthen);
                        transferAfter = newItem1;

                    }
                   
                    StrengthenMgr.InheritTransferProperty(ref transferBefore, ref transferAfter, _moveHole, _moveFivSixHole);
                    
                    client.Player.StoreBag2.ClearBag();                    
                    client.Player.StoreBag2.AddItemTo(transferBefore, 0);
                    client.Player.StoreBag2.AddItemTo(transferAfter, 1);
                    client.Player.SaveIntoDatabase();
                    pkg.WriteByte(0);
                    client.Out.SendTCP(pkg);
               
            }
            else
            {
                client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("itemtransferhandler.nocondition"));
            }
           
            return 0;
        }
    }
}
