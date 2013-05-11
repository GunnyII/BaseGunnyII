using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Bussiness;
using SqlDataProvider.Data;
using Game.Server.Managers;
using Bussiness.Managers;
using Game.Server.Statics;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.CARDS_DATA, "防沉迷系统开关")]
    class CardDataHander : IPacketHandler
    {
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {

            //return 1;
            var cmdCard = packet.ReadInt();
            var param1 = packet.ReadInt();
            var param2 = 0;
            var cardBag = client.Player.CardBag;
            ItemInfo item = null;
            Random randomExp = new Random();
            cardBag.BeginChanges();
            switch (cmdCard)
            {
                case 0:
                    //move card
                    param2 = packet.ReadInt();
                    if (param1 > 4)
                    {
                        if (cardBag.GetItemAt(param1) != null)
                        {
                            cardBag.MoveItem(param1, param2, cardBag.GetItemAt(param1).Count);
                        }
                    }
                    else
                    {
                        item = cardBag.GetItemAt(param1);
                        if (item == null) return 1;
                        if (cardBag.GetItemByTemplateID(5, item.TemplateID) != null)
                        {
                            var itemUpdate = cardBag.GetItemByTemplateID(5, item.TemplateID);
                            itemUpdate.Count++;
                            cardBag.UpdateItem(itemUpdate);
                            cardBag.RemoveItem(item);
                            break;
                        }
                        else
                        {
                            cardBag.MoveItem(item.Place,cardBag.FindFirstEmptySlot(5),item.Count);
                        }
                    }
                    client.Player.MainBag.UpdatePlayerProperties();
                    //client.Player.SaveIntoDatabase();
                    break;
                case 1:
                    //open vice card
                    //_loc_2.writeInt(1);
                    //_loc_2.writeInt(param1);

                    break;
                case 2:
                    //OpenCardBox
                    param2 = packet.ReadInt();
                    item = client.Player.MainBag.GetItemAt(param1);
                    client.Player.MainBag.RemoveItem(item);
                    var cardTemplateID = item.Template.Property5;
                    ItemMgr.FindItemTemplate(cardTemplateID);
                    //neu co' roi` thi cong them vo
                    if (cardBag.GetItemByTemplateID(5,cardTemplateID)!=null)
                    {
                        var itemUpdate=cardBag.GetItemByTemplateID(5,cardTemplateID);
                        itemUpdate.Count++;
                        cardBag.UpdateItem(itemUpdate);
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("Bạn nhận được " + ItemMgr.FindItemTemplate(cardTemplateID).Name));
                        break;
                    }
                    //neu chua co'
                    var index = cardBag.FindFirstEmptySlot(5);
                    cardBag.AddItemTo(ItemInfo.CreateFromTemplate(ItemMgr.FindItemTemplate(cardTemplateID), 1, (int)ItemAddType.Buy), index);
                    break;
                case 3:
                    //UpgradeCard
                    item = cardBag.GetItemAt(param1);
                    if (item != null && item.Count > 3)
                    {
                        item.Count -= 3;
                        switch (item.StrengthenLevel)
                        {
                            case 0:
                                item.Hole5Exp += randomExp.Next(2, 6);
                                break;
                            case 1:
                                item.Hole5Exp += randomExp.Next(3, 7);
                                break;
                            case 2:
                                item.Hole5Exp += randomExp.Next(5, 9);
                                break;
                        }
                        if (item.Hole5Exp >= 500 && item.StrengthenLevel == 0)
                        {
                            item.StrengthenLevel ++; //2-5p: 500p up
                            //item.Hole5Exp = 0;
                        }
                        else if (item.Hole5Exp >= 2000 && item.StrengthenLevel == 1)
                        {
                            item.StrengthenLevel ++;//3-6p: 1500 up
                            //item.Hole5Exp = 0;
                        }
                        else if (item.Hole5Exp >= 7000 && item.StrengthenLevel == 2)
                        {
                            item.StrengthenLevel ++;//5-8p: 5000 up
                            //item.Hole5Exp = 0;
                        }
                        cardBag.UpdateItem(item);
                    }
                    break;
                case 4:
                    //CardSort
                    //_loc_2.writeInt(4);
                   // var _loc_3:* = param1.length;
                    //_loc_2.writeInt(_loc_3);
                    //var _loc_4:int = 0;
                    //while (_loc_4 < _loc_3)
                    //{
                
                //_loc_5 = param1[_loc_4];
                //_loc_6 = _loc_4 + CardModel.EQUIP_CELLS_SUM;
                //_loc_2.writeInt(_loc_5);
                //_loc_2.writeInt(_loc_6);
                //_loc_4 = _loc_4 + 1;
                //    }
                //case 5:
                    //FirstGetCard
                    //item = cardBag.GetItemAt(param1);
                    //item.IsGold = true;
                    //break;

                default:
                    break;
            }
           
            cardBag.CommitChanges();
			cardBag.SaveToDatabase();           
            return 0;
        }
    }
}
