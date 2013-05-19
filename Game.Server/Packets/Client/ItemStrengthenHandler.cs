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
using Game.Server.GameObjects;
using Game.Server.GameUtils;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.ITEM_STRENGTHEN, "物品强化")]
    public class ItemStrengthenHandler : IPacketHandler
    {
        //private static readonly double[] rateItems = new double[] { 0.75, 3, 12, 48, 240, 768 };
        //public static int countConnect = 0;
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {

            //if (countConnect >= 3000)
            //{
            //    client.Disconnect();
            //    return 0;
            //}
            StringBuilder str = new StringBuilder();
            bool isBinds = false;
            bool consortia = packet.ReadBoolean();
            bool MultiSelected = packet.ReadBoolean();

            GSPacketIn pkg = packet.Clone();
            pkg.ClearContext();           

            List<ItemInfo> stones = new List<ItemInfo>();
            ItemInfo stone = client.Player.StoreBag2.GetItemAt(0);
            ItemInfo item = client.Player.StoreBag2.GetItemAt(1);
            
            //ItemInfo luck = null;
            //ItemInfo god = null;
            string BeginProperty = null;
            string AddItem = "";
            using (ItemRecordBussiness db = new ItemRecordBussiness())
            {
                db.PropertyString(item, ref BeginProperty);
            }

            if (item != null && item.Template.CanStrengthen && item.Template.CategoryID < 18 && item.Count == 1)
            {
                isBinds = isBinds ? true : item.IsBinds;
                str.Append(item.ItemID + ":" + item.TemplateID + ",");                
                double exp1 = 0;
                double exp2 = 0;
                double exp3 = 0;
                double totalExp = 0;              
                                
                if (stone != null && stone.Template.CategoryID == 11 && (stone.Template.Property1 == 2 || stone.Template.Property1 == 35))
                {
                    isBinds = isBinds ? true : stone.IsBinds;
                    AddItem += "," + stone.ItemID.ToString() + ":" + stone.Template.Name;
                    stones.Add(stone);
                    exp1 += stone.Template.Property2;
                }

                bool ConsortiaRate = false;
                ConsortiaInfo info = ConsortiaMgr.FindConsortiaInfo(client.Player.PlayerCharacter.ConsortiaID);
                //判断是公会铁匠铺还是铁匠铺??
                if (consortia)
                {
                    ConsortiaBussiness csbs = new ConsortiaBussiness();
                    ConsortiaEquipControlInfo cecInfo = csbs.GetConsortiaEuqipRiches(client.Player.PlayerCharacter.ConsortiaID, 0, 2);

                    if (info == null)
                    {
                        client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemStrengthenHandler.Fail"));
                    }
                    else
                    {
                        if (client.Player.PlayerCharacter.Riches < cecInfo.Riches)
                        {
                            client.Out.SendMessage(eMessageType.ERROR, LanguageMgr.GetTranslation("ItemStrengthenHandler.FailbyPermission"));
                            return 1;
                        }
                        ConsortiaRate = true;
                    }
                }
                if (ConsortiaRate)
                {
                    //ConsortiaRateManager.instance.getConsortiaStrengthenEx(PlayerManager.Instance.Self.consortiaInfo.SmithLevel)
                    //"ConsortiaStrengthenEx" Value="10|20|30|40|50|60|70|80|90|100"
                    List<double> ConsortiaStrengthenEx = new List<double> { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
                    exp2 = ConsortiaStrengthenEx[info.SmithLevel - 1] / 100 * exp1;
                }
                if (client.Player.PlayerCharacter.VIPExpireDay >= DateTime.Now)
                {
                    //_loc_4 = VipController.instance.getVIPStrengthenEx(PlayerManager.Instance.Self.VIPLevel)
                    //"VIPStrengthenEx" Value="25|25|25|35|35|50|50|50|50|50|50|50"
                    List<double> VIPStrengthenEx = new List<double> { 25, 25, 25, 35, 35, 50, 50, 50, 50, 50, 50, 50 };
                    exp3 = VIPStrengthenEx[client.Player.PlayerCharacter.VIPLevel - 1] / 100 * exp1;
                }
                totalExp += Math.Floor(exp1 + exp2 + exp3);
                str.Append("true");
                List<int> StrengThenExp = new List<int> { 0, 10, 50, 150, 350, 700, 1500, 2300, 3300, 4500, 6000, 7500, 9000 };
				
                //Console.WriteLine("-------Total: " + stone.Count.ToString() + "| Inject: " + MultiSelected);
                if (MultiSelected)// && stone.Count > 1)
                {
                    //for (int i = 0; i < stone.Count; i++)
                    //{
                    item.StrengthenExp += (int)totalExp * stone.Count;
                    client.Player.StoreBag2.RemoveTemplate(stone.TemplateID, stone.Count); 
                    //}
                }
                else
                {
                    item.StrengthenExp += (int)totalExp;
                    client.Player.StoreBag2.RemoveTemplate(stone.TemplateID, 1);
                   
                }

                if (item.StrengthenExp >= StrengThenExp[item.StrengthenLevel + 1])
                {
                    //for (int a = StrengThenExp[item.StrengthenLevel]; a < item.StrengthenExp; a++)
                    int a = StrengThenExp[item.StrengthenLevel];
                    do
                    {
                        if (item.StrengthenLevel < 13)
                        {
                            if (item.StrengthenExp >= StrengThenExp[item.StrengthenLevel + 1])
                            {
                                item.StrengthenLevel++;
                                item.StrengthenExp -= StrengThenExp[item.StrengthenLevel];
                                a = StrengThenExp[item.StrengthenLevel];

                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    while (item.StrengthenExp > a);
                    if (item.StrengthenLevel == 12 && (item.StrengthenExp / totalExp) > 0)
                    {
                        stone.Count = (int)Math.Floor(item.StrengthenExp / totalExp);
                        client.Player.StoreBag2.AddItemTo(stone, 0);
                        client.Player.StoreBag2.UpdateItem(stone);
                        //if (item.StrengthenLevel == 12)
                        item.StrengthenExp = 0;
                    }
                    pkg.WriteByte(1);

                    StrengthenGoodsInfo strengthenGoodsInfo = StrengthenMgr.FindStrengthenGoodsInfo(item.StrengthenLevel, item.TemplateID);
                    if (strengthenGoodsInfo != null && item.Template.CategoryID == 7)
                    {
                        ItemTemplateInfo Temp = Bussiness.Managers.ItemMgr.FindItemTemplate(strengthenGoodsInfo.GainEquip);
                        if (Temp != null)
                        {
                            ItemInfo newItem = ItemInfo.CreateFromTemplate(Temp, 1, (int)ItemAddType.Strengthen);
                            newItem.StrengthenLevel = item.StrengthenLevel;
                            newItem.StrengthenExp = item.StrengthenExp;
                            ItemInfo.OpenHole(ref newItem);
                            StrengthenMgr.InheritProperty(item, ref newItem);
                            client.Player.StoreBag2.RemoveItemAt(1);
                            client.Player.StoreBag2.AddItemTo(newItem, 1);
                            //client.Player.StoreBag2.UpdateItem(newItem);
                            item = newItem;
                            if ((item.StrengthenLevel == 3 || item.StrengthenLevel == 6 || item.StrengthenLevel == 9 || item.StrengthenLevel == 12) && item.Template.CategoryID != 17)
                            {
                                pkg.WriteBoolean(true);
                            }
                            else
                            {
                                pkg.WriteBoolean(false);
                            }
                        }
                    }
                    else
                    {

                        ItemInfo.OpenHole(ref item);
                        //client.Player.StoreBag2.AddItemTo(item, 1);
                        if ((item.StrengthenLevel == 3 || item.StrengthenLevel == 6 || item.StrengthenLevel == 9 || item.StrengthenLevel == 12) && item.Template.CategoryID != 17)
                        {
                            pkg.WriteBoolean(true);
                        }
                        else
                        {
                            pkg.WriteBoolean(false);
                        }
                    }
                    
                    //系统广播
                    if (item.StrengthenLevel >= 7)
                    {
                        string msg = LanguageMgr.GetTranslation("ItemStrengthenHandler.congratulation", client.Player.PlayerCharacter.NickName, item.Template.Name, item.StrengthenLevel);
                        GSPacketIn sys_notice = WorldMgr.SendSysNotice(msg);
                        GameServer.Instance.LoginServer.SendPacket(sys_notice);

                    }
                }
                else
                {
                    pkg.WriteByte(1);
                    pkg.WriteBoolean(false);
                }
                
                client.Player.StoreBag2.UpdateItem(item);
                client.Player.OnItemStrengthen(item.Template.CategoryID, item.StrengthenLevel);//任务<强化>                                               
                LogMgr.LogItemAdd(client.Player.PlayerCharacter.ID, LogItemType.Strengthen, BeginProperty, item, AddItem, 1);//强化日志
                
                //client.Player.SaveIntoDatabase();//保存到数据库
                client.Out.SendTCP(pkg);
                str.Append(item.StrengthenLevel);
                //client.Player.BeginChanges();
                //client.Player.CommitChanges();
            }
            else
            {
                client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ItemStrengthenHandler.Content1") + stone.Template.Name + LanguageMgr.GetTranslation("ItemStrengthenHandler.Content2"));
            }
            if (item.Place < 31)
            {
                client.Player.MainBag.UpdatePlayerProperties();
            }           

            return 0;
        }
    }
}
