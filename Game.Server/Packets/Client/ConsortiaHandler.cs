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
            var consortia_cmd = packet.ReadInt();
           
            switch (consortia_cmd)
            {
                case (int)ConsortiaPackageType.CONSORTIA_TRYIN:
                    HandleCONSORTIA_TRYIN(packet);
                    break;
                case (int)ConsortiaPackageType.CONSORTIA_CREATE:
                    HandleCONSORTIA_CREATE(packet);
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
