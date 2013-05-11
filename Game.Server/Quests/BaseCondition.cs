using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlDataProvider.Data;
using Game.Server.GameObjects;
using log4net;
using System.Reflection;

namespace Game.Server.Quests
{
    public class BaseCondition
    {
        /// <summary>
        /// 任务条件
        /// </summary>
        protected QuestConditionInfo m_info;
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        ///  完成条件数量
        /// </summary>
        private int m_value;

        private BaseQuest m_quest;

        public BaseCondition(BaseQuest quest,QuestConditionInfo info, int value)
        {
            m_quest = quest;
            m_info = info;
            m_value = value;
        }

        public QuestConditionInfo Info
        {
            get { return m_info; }
        }

        public int Value
        {
            get { return m_value; }
            set 
            {
                if (m_value != value)
                {
                    m_value = value;
                    m_quest.Update();
                }
            }
        }

        public virtual void Reset(GamePlayer player)
        {
            m_value = m_info.Para2;
        }

        public virtual void AddTrigger(GamePlayer player)
        {
        }

        public virtual void RemoveTrigger(GamePlayer player)
        {
 
        }

        public virtual bool IsCompleted(GamePlayer player)
        {
            return false;
        }

        public virtual bool Finish(GamePlayer player)
        {
            return true;
        }

        public virtual bool CancelFinish(GamePlayer player)
        {
            return true;
        }

        public static BaseCondition CreateCondition(BaseQuest quest, QuestConditionInfo info, int value)
        {
            switch (info.CondictionType)
            {
                case 1:
                    return new OwnGradeCondition(quest, info, value);        //1、升级/空/等级数
                case 2:
                    return new ItemMountingCondition(quest, info, value);    //2、使用物品
                case 3:
                    return new UsingItemCondition(quest, info, value);       //3、使用指定道具/道具ID/数量
                case 4:
                    return new GameKillByRoomCondition(quest, info, value);  //4、击杀玩家若干人次/房间模式（-1不限，0撮合，1自由，2练级，3副本）/数量
                case 5:
                    return new GameFightByRoomCondition(quest, info, value); //5、完成战斗（无论胜败）/房间模式/数量
                case 6:
                    return new GameOverByRoomCondition(quest, info, value);  //6、战斗胜利/房间模式/数量
                case 7:
                    return new GameCopyOverCondition(quest, info, value);    //7、完成副本（无论胜败）/副本ID/次数
                case 8:
                    return new GameCopyPassCondition(quest, info, value);    //8、通关副本（要求胜利）/副本ID/次数
                case 9:
                    return new ItemStrengthenCondition(quest, info, value);  //9、强化/装备类型/强化等级
                case 10:
                    return new ShopCondition(quest, info, value);            //10、购买/货币类型/支付金额
                case 11:
                    return new ItemFusionCondition(quest, info, value);      //11、熔炼成功/熔炼类型/次数
                case 12:
                    return new ItemMeltCondition(quest, info, value);        //12、炼化/装备类型/炼化等级
                case 13:
                    return new GameMonsterCondition(quest, info, value);     //13、击杀怪物/怪物ID/数量
                case 14:
                    return new OwnPropertyCondition(quest, info, value);     //14、拥有道具（完成任务道具不消失）/道具ID/数量
                case 15:
                    return new TurnPropertyCondition(quest, info, value);    //15、上缴道具（完成任务道具消失）/道具ID/数量
                case 16:
                    return new DirectFinishCondition(quest, info, value);    //16、直接完成/空/1
                case 17:
                    return new OwnMarryCondition(quest, info, value);        //17、结婚/空/1
                case 18:
                    return new OwnConsortiaCondition(quest, info, value);    //18、公会人数/空/具体人数
                case 19:
                    return new ItemComposeCondition(quest, info, value);     //19、合成/合成类型/次数
                case 20:
                    return new ClientModifyCondition(quest, info, value);    //20、客户端请求
                case 21:
                    return new GameMissionOverCondition(quest, info, value); //21、通关关卡/关卡ID/回合数
                case 22:
                    return new GameKillByGameCondition(quest, info, value);  //22、击杀玩家若干人次/游戏模式/数量
                case 23:
                    return new GameFightByGameCondition(quest, info, value); //23、完成战斗（无论胜败）/游戏模式/数量
                case 24:
                    return new GameOverByGameCondition(quest, info, value);  //24、战斗胜利/游戏模式/数量
                case 25:
                    return new ItemInsertCondition(quest, info, value); //25、Khảm nạm châu báu
                case 26://hoan thanh vo chong
                case 27://enter spa
                case 28://vo chong chien thang               
                case 30://Hoàn thành 5 thành tựu   

                case 31://Chiến thắng 1 trận
                case 32://Chia sẻ 3 lần trạng thái cá nhân
                case 34://Tham gia 2 trận chiến đấu 2v2

                case 35://Quest sư đồ

                case 36://Quest chiến đấu sư đồ 
                case 37://Cùng sư phụ hoàn thành trận Sư đồ chiến
                case 38://Quest nạp thẻ
                case 39://Trang bị mới
                case 42://Điền số điện thoại và mã kiểm chứng
                case 43://Quest Hoàn thành đăng nhập

                case 44://Thiết lập mật mã cấp 2 để hoàn thành nhiệm vụ
                case 45://Chiến đấu cùng thú cưng
                case 46://Hợp thành 1 thức ăn thú cưng
                case 47://Đến Nông Trại bạn bè trộm rau
                case 48://Nhận nuôi 1 thú cưng
                case 49://Thu hoạch Lúa Mì
                case 50://Thăng cấp thú cưng lên cấp 5
                case 51://Hạt giống thức ăn thú cưng
                default:
                    if (log.IsErrorEnabled)
                        log.Warn(string.Format("Can't find quest condition : {0}", info.CondictionType));
                    return null;                    
            }
        }
    }
}
