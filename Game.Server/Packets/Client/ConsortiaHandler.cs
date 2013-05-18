using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Base.Packets;
using Bussiness;
using SqlDataProvider.Data;
using Game.Server.GameObjects;
using Game.Server.Managers;

namespace Game.Server.Packets.Client
{
    [PacketHandler((int)ePackageType.CONSORTIA_CMD, "公会聊天")]
    public class ConsortiaHandler : IPacketHandler
    {        
        public int HandlePacket(GameClient client, GSPacketIn packet)
        {
            var consortiaCmd = packet.ReadInt();

            bool result = false;
            string msg = "Packet Error!";
            ConsortiaLevelInfo levelInfo = null;
            GamePlayer[] players = WorldMgr.GetAllPlayers();

            switch (consortiaCmd)
            {
                case (int)ConsortiaPackageType.CONSORTIA_TRYIN:
                    HandleCONSORTIA_TRYIN(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_CREATE:
                    //Create Guild
                    {
                        if (client.Player.PlayerCharacter.ConsortiaID != 0)
                            return 0;
                        levelInfo = ConsortiaLevelMgr.FindConsortiaLevelInfo(1);
                        string name = packet.ReadString();
                        //if (string.IsNullOrEmpty(name) || System.Text.Encoding.Default.GetByteCount(name) > 12)
                        //{
                        //    client.Out.SendMessage(eMessageType.Normal, LanguageMgr.GetTranslation("ConsortiaCreateHandler.Long"));
                        //    return 1;
                        //}                    
                        //result = false;
                        int id = 0;
                        int mustGold = levelInfo.NeedGold;
                        int mustLevel = 5;
                        msg = "ConsortiaCreateHandler.Failed";
                        ConsortiaDutyInfo dutyInfo = new ConsortiaDutyInfo();

                        if (!string.IsNullOrEmpty(name) && client.Player.PlayerCharacter.Gold >= mustGold && client.Player.PlayerCharacter.Grade >= mustLevel)
                        {
                            using (ConsortiaBussiness db = new ConsortiaBussiness())
                            {
                                ConsortiaInfo info = new ConsortiaInfo();
                                info.BuildDate = DateTime.Now;
                                info.CelebCount = 0;
                                info.ChairmanID = client.Player.PlayerCharacter.ID;
                                info.ChairmanName = client.Player.PlayerCharacter.NickName;
                                info.ConsortiaName = name;
                                info.CreatorID = info.ChairmanID;
                                info.CreatorName = info.ChairmanName;
                                info.Description = "";
                                info.Honor = 0;
                                info.IP = "";
                                info.IsExist = true;
                                info.Level = levelInfo.Level;
                                info.MaxCount = levelInfo.Count;
                                info.Riches = levelInfo.Riches;
                                info.Placard = "";
                                info.Port = 0;
                                info.Repute = 0;
                                info.Count = 1;

                                if (db.AddConsortia(info, ref msg, ref dutyInfo))
                                {
                                    client.Player.PlayerCharacter.ConsortiaID = info.ConsortiaID;
                                    client.Player.PlayerCharacter.ConsortiaName = info.ConsortiaName;
                                    client.Player.PlayerCharacter.DutyLevel = dutyInfo.Level;
                                    client.Player.PlayerCharacter.DutyName = dutyInfo.DutyName;
                                    client.Player.PlayerCharacter.Right = dutyInfo.Right;
                                    client.Player.PlayerCharacter.ConsortiaLevel = levelInfo.Level;
                                    client.Player.RemoveGold(mustGold);
                                    msg = "ConsortiaCreateHandler.Success";
                                    result = true;
                                    id = info.ConsortiaID;
                                    GameServer.Instance.LoginServer.SendConsortiaCreate(id, client.Player.PlayerCharacter.Offer, info.ConsortiaName);
                                }
                                else
                                {
                                    client.Player.SendMessage("db.AddConsortia Error ");
                                }
                            }

                        }
                        //Send package create Guild
                        client.Out.SendConsortiaCreate(name, result, id, name, LanguageMgr.GetTranslation(msg), dutyInfo.Level, dutyInfo.DutyName, dutyInfo.Right, client.Player.PlayerCharacter.ID);
                    }
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_DISBAND:
                    HandleCONSORTIA_DISBAND(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_RENEGADE:
                    HandleCONSORTIA_RENEGADE(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_TRYIN_PASS:
                    HandleCONSORTIA_TRYIN_PASS(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_TRYIN_DEL:
                    HandleCONSORTIA_TRYIN_DEL(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_RICHES_OFFER:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_APPLY_STATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_DUTY_DELETE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_DUTY_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_INVITE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_INVITE_PASS:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_INVITE_DELETE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_DESCRIPTION_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_PLACARD_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_BANCHAT_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_USER_REMARK_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_USER_GRADE_UPDATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_CHAIRMAN_CHAHGE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_CHAT:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_LEVEL_UP:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_TASK_RELEASE:
                    break;
                case (int)ConsortiaPackageType.DONATE:
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_EQUIP_CONTROL:
                    break;
                case (int)ConsortiaPackageType.POLL_CANDIDATE:
                    break;
                case (int)ConsortiaPackageType.SKILL_SOCKET:
                    break;
                case (int)ConsortiaPackageType.CONSORTION_MAIL:
                    break;
                case (int)ConsortiaPackageType.BUY_BADGE:
                    break;
            }

            return 0;
        }
        protected void HandleCONSORTIA_TRYIN(GSPacketIn packet)
        {

        }
        protected void HandleCONSORTIA_CREATE(GSPacketIn packet)
        {

        }
        protected void HandleCONSORTIA_DISBAND(GSPacketIn packet)
        {

        }
        protected void HandleCONSORTIA_RENEGADE(GSPacketIn packet)
        {

        }
        protected void HandleCONSORTIA_TRYIN_PASS(GSPacketIn packet)
        {

        }
        protected void HandleCONSORTIA_TRYIN_DEL(GSPacketIn packet)
        {

        }
    }
}
