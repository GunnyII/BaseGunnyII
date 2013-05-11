using Bussiness;
using log4net;
using SqlDataProvider.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;

namespace Bussiness.Managers
{

    public class AchievementMgr
    {
        // Fields
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<int, AchievementInfo> m_achievement = new Dictionary<int, AchievementInfo>();
        private static Dictionary<int, List<AchievementConditionInfo>> m_achievementCondition = new Dictionary<int, List<AchievementConditionInfo>>();
        private static Dictionary<int, List<AchievementRewardInfo>> m_achievementReward = new Dictionary<int, List<AchievementRewardInfo>>();
        private static Hashtable m_distinctCondition = new Hashtable();
        private static Dictionary<int, List<ItemRecordTypeInfo>> m_itemRecordType = new Dictionary<int, List<ItemRecordTypeInfo>>();
        private static Hashtable m_ItemRecordTypeInfo = new Hashtable();
        private static Dictionary<int, List<int>> m_recordLimit = new Dictionary<int, List<int>>();

        // Methods
        public static List<AchievementConditionInfo> GetAchievementCondition(AchievementInfo info)
        {
            if (m_achievementCondition.ContainsKey(info.ID))
            {
                return m_achievementCondition[info.ID];
            }
            return null;
        }

        public static List<AchievementRewardInfo> GetAchievementReward(AchievementInfo info)
        {
            if (m_achievementReward.ContainsKey(info.ID))
            {
                return m_achievementReward[info.ID];
            }
            return null;
        }

        public static int GetNextLimit(int recordType, int recordValue)
        {
            if (m_recordLimit.ContainsKey(recordType))
            {
                foreach (int num in m_recordLimit[recordType])
                {
                    if (num > recordValue)
                    {
                        return num;
                    }
                }
                return 2147483647;
            }
            return 2147483647;
        }

        public static AchievementInfo GetSingleAchievement(int id)
        {
            if (m_achievement.ContainsKey(id))
            {
                return m_achievement[id];
            }
            return null;
        }

        public static bool Init()
        {
            return Reload();
        }

        public static Dictionary<int, List<AchievementConditionInfo>> LoadAchievementConditionInfoDB(Dictionary<int, AchievementInfo> achievementInfos)
        {
            Dictionary<int, List<AchievementConditionInfo>> dictionary = new Dictionary<int, List<AchievementConditionInfo>>();
            using (ProduceBussiness bussiness = new ProduceBussiness())
            {
                AchievementConditionInfo[] aLlAchievementCondition = bussiness.GetALlAchievementCondition();
                using (Dictionary<int, AchievementInfo>.ValueCollection.Enumerator enumerator = achievementInfos.Values.GetEnumerator())
                {
                    Func<AchievementConditionInfo, bool> predicate = null;
                    AchievementInfo achievementInfo;
                    while (enumerator.MoveNext())
                    {
                        achievementInfo = enumerator.Current;
                        if (predicate == null)
                        {
                            predicate = s => s.AchievementID == achievementInfo.ID;
                        }
                        IEnumerable<AchievementConditionInfo> source = aLlAchievementCondition.Where<AchievementConditionInfo>(predicate);
                        dictionary.Add(achievementInfo.ID, source.ToList<AchievementConditionInfo>());
                        if (source != null)
                        {
                            foreach (AchievementConditionInfo info in source)
                            {
                                if (!m_distinctCondition.Contains(info.CondictionType))
                                {
                                    m_distinctCondition.Add(info.CondictionType, info.CondictionType);
                                }
                            }
                        }
                    }
                }
                foreach (AchievementConditionInfo info2 in aLlAchievementCondition)
                {
                    int condictionType = info2.CondictionType;
                    int item = info2.Condiction_Para2;
                    if (!m_recordLimit.ContainsKey(condictionType))
                    {
                        m_recordLimit.Add(condictionType, new List<int>());
                    }
                    if (!m_recordLimit[condictionType].Contains(item))
                    {
                        m_recordLimit[condictionType].Add(item);
                    }
                }
                foreach (int num in m_recordLimit.Keys)
                {
                    m_recordLimit[num].Sort();
                }
            }
            return dictionary;
        }

        public static Dictionary<int, AchievementInfo> LoadAchievementInfoDB()
        {
            Dictionary<int, AchievementInfo> dictionary = new Dictionary<int, AchievementInfo>();
            using (ProduceBussiness bussiness = new ProduceBussiness())
            {
                AchievementInfo[] aLlAchievement = bussiness.GetALlAchievement();
                foreach (AchievementInfo info in aLlAchievement)
                {
                    if (!dictionary.ContainsKey(info.ID))
                    {
                        dictionary.Add(info.ID, info);
                    }
                }
            }
            return dictionary;
        }

        public static Dictionary<int, List<AchievementRewardInfo>> LoadAchievementRewardInfoDB(Dictionary<int, AchievementInfo> achievementInfos)
        {
            Dictionary<int, List<AchievementRewardInfo>> dictionary = new Dictionary<int, List<AchievementRewardInfo>>();
            using (ProduceBussiness bussiness = new ProduceBussiness())
            {
                AchievementRewardInfo[] aLlAchievementReward = bussiness.GetALlAchievementReward();
                using (Dictionary<int, AchievementInfo>.ValueCollection.Enumerator enumerator = achievementInfos.Values.GetEnumerator())
                {
                    Func<AchievementRewardInfo, bool> predicate = null;
                    AchievementInfo achievementInfo;
                    while (enumerator.MoveNext())
                    {
                        achievementInfo = enumerator.Current;
                        if (predicate == null)
                        {
                            predicate = s => s.AchievementID == achievementInfo.ID;
                        }
                        IEnumerable<AchievementRewardInfo> source = aLlAchievementReward.Where<AchievementRewardInfo>(predicate);
                        dictionary.Add(achievementInfo.ID, source.ToList<AchievementRewardInfo>());
                    }
                }
            }
            return dictionary;
        }

        public static void LoadItemRecordTypeInfoDB()
        {
            using (ProduceBussiness bussiness = new ProduceBussiness())
            {
                ItemRecordTypeInfo[] allItemRecordType = bussiness.GetAllItemRecordType();
                foreach (ItemRecordTypeInfo info in allItemRecordType)
                {
                    if (!m_ItemRecordTypeInfo.Contains(info.RecordID))
                    {
                        m_ItemRecordTypeInfo.Add(info.RecordID, info.Name);
                    }
                }
            }
        }

        public static bool Reload()
        {
            try
            {
                LoadItemRecordTypeInfoDB();
                Dictionary<int, AchievementInfo> achievementInfos = LoadAchievementInfoDB();
                Dictionary<int, List<AchievementConditionInfo>> dictionary2 = LoadAchievementConditionInfoDB(achievementInfos);
                Dictionary<int, List<AchievementRewardInfo>> dictionary3 = LoadAchievementRewardInfoDB(achievementInfos);
                if (achievementInfos.Count > 0)
                {
                    Interlocked.Exchange<Dictionary<int, AchievementInfo>>(ref m_achievement, achievementInfos);
                    Interlocked.Exchange<Dictionary<int, List<AchievementConditionInfo>>>(ref m_achievementCondition, dictionary2);
                    Interlocked.Exchange<Dictionary<int, List<AchievementRewardInfo>>>(ref m_achievementReward, dictionary3);
                }
                return true;
            }
            catch (Exception exception)
            {
                log.Error("AchievementMgr", exception);
            }
            return false;
        }

        // Properties
        public static Dictionary<int, AchievementInfo> Achievement
        {
            get
            {
                return m_achievement;
            }
        }

        public static Hashtable ItemRecordType
        {
            get
            {
                return m_ItemRecordTypeInfo;
            }
        }
    }

}

