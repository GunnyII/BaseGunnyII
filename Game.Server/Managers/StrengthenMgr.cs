using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.Reflection;
using log4net.Util;
using Game.Server.GameObjects;
using System.Threading;
using Bussiness;
using SqlDataProvider.Data;

namespace Game.Server.Managers
{
    public class StrengthenMgr
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<int, StrengthenInfo> _strengthens;

        private static Dictionary<int, StrengthenInfo> m_Refinery_Strengthens;

        private static Dictionary<int, StrengthenGoodsInfo> Strengthens_Goods;

        private static System.Threading.ReaderWriterLock m_lock;

        private static ThreadSafeRandom rand;

        public static bool ReLoad()
        {
            try
            {
                Dictionary<int, StrengthenInfo> tempStrengthens = new Dictionary<int, StrengthenInfo>();
                Dictionary<int, StrengthenInfo> tempRefineryStrengthens = new Dictionary<int, StrengthenInfo>();
                Dictionary<int, StrengthenGoodsInfo> tempStrengthenGoodsInfos = new Dictionary<int, StrengthenGoodsInfo>();
                if (LoadStrengthen(tempStrengthens, tempRefineryStrengthens))
                {
                    m_lock.AcquireWriterLock(Timeout.Infinite);
                    try
                    {
                        _strengthens = tempStrengthens;
                        m_Refinery_Strengthens = tempRefineryStrengthens;
                        Strengthens_Goods = tempStrengthenGoodsInfos;
                        return true;
                    }
                    catch
                    { }
                    finally
                    {
                        m_lock.ReleaseWriterLock();
                    }

                }
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("StrengthenMgr", e);
            }

            return false;
        }

        /// <summary>
        /// Initializes the StrengthenMgr. 
        /// </summary>
        /// <returns></returns>
        public static bool Init()
        {
            try
            {
                m_lock = new System.Threading.ReaderWriterLock();
                _strengthens = new Dictionary<int, StrengthenInfo>();
                m_Refinery_Strengthens = new Dictionary<int, StrengthenInfo>();
                Strengthens_Goods = new Dictionary<int, StrengthenGoodsInfo>();
                rand = new ThreadSafeRandom();
                return LoadStrengthen(_strengthens, m_Refinery_Strengthens);
            }
            catch (Exception e)
            {
                if (log.IsErrorEnabled)
                    log.Error("StrengthenMgr", e);
                return false;
            }

        }

        private static bool LoadStrengthen(Dictionary<int, StrengthenInfo> strengthen, Dictionary<int, StrengthenInfo> RefineryStrengthen)
        {
            using (ProduceBussiness db = new ProduceBussiness())
            {
                StrengthenInfo[] infos = db.GetAllStrengthen();

                StrengthenInfo[] Refineryinfos = db.GetAllRefineryStrengthen();

                StrengthenGoodsInfo[] StrengthGoodInfos = db.GetAllStrengthenGoodsInfo();

                foreach (StrengthenInfo info in infos)
                {
                    if (!strengthen.ContainsKey(info.StrengthenLevel))
                    {
                        strengthen.Add(info.StrengthenLevel, info);
                    }
                }
                foreach (StrengthenInfo info in Refineryinfos)
                {
                    if (!RefineryStrengthen.ContainsKey(info.StrengthenLevel))
                    {
                        RefineryStrengthen.Add(info.StrengthenLevel, info);
                    }
                }

                foreach (StrengthenGoodsInfo info in StrengthGoodInfos)
                {
                    if (!Strengthens_Goods.ContainsKey(info.ID))
                    {
                        Strengthens_Goods.Add(info.ID, info);
                    }
                }
            }

            return true;
        }

        public static StrengthenInfo FindStrengthenInfo(int level)
        {
            m_lock.AcquireReaderLock(Timeout.Infinite);
            try
            {
                if (_strengthens.ContainsKey(level))
                    return _strengthens[level];
            }
            catch
            { }
            finally
            {
                m_lock.ReleaseReaderLock();
            }
            return null;
        }

        public static StrengthenInfo FindRefineryStrengthenInfo(int level)
        {
            m_lock.AcquireReaderLock(Timeout.Infinite);
            try
            {
                if (m_Refinery_Strengthens.ContainsKey(level))
                    return m_Refinery_Strengthens[level];
            }
            catch
            { }
            finally
            {
                m_lock.ReleaseReaderLock();
            }
            return null;
        }
        public static StrengthenGoodsInfo FindStrengthenGoodsInfo(int level, int TemplateId)
        {
            m_lock.AcquireReaderLock(Timeout.Infinite);
            try
            {
                foreach (int i in Strengthens_Goods.Keys)
                {
                    if (Strengthens_Goods[i].Level == level && (TemplateId == Strengthens_Goods[i].CurrentEquip || TemplateId == Strengthens_Goods[i].GainEquip))
                        return Strengthens_Goods[i];

                }
                //if (Strengthens_Goods.ContainsKey(level))
                //    return Strengthens_Goods[level];
            }
            catch
            { }
            finally
            {
                m_lock.ReleaseReaderLock();
            }
            return null;
        }


        public static void InheritProperty(ItemInfo Item, ref ItemInfo item)
        {
            if (Item.Hole1 >= 0)
                item.Hole1 = Item.Hole1;
            if (Item.Hole2 >= 0)
                item.Hole2 = Item.Hole2;
            if (Item.Hole3 >= 0)
                item.Hole3 = Item.Hole3;
            if (Item.Hole4 >= 0)
                item.Hole4 = Item.Hole4;
            if (Item.Hole5 >= 0)
                item.Hole5 = Item.Hole5;
            if (Item.Hole6 >= 0)
                item.Hole6 = Item.Hole6;

            item.AttackCompose = Item.AttackCompose;
            item.DefendCompose = Item.DefendCompose;
            item.LuckCompose = Item.LuckCompose;
            item.AgilityCompose = Item.AgilityCompose;
            item.IsBinds = Item.IsBinds;
            item.ValidDate = Item.ValidDate;
        }

        public static void InheritTransferProperty(ref ItemInfo item0, ref ItemInfo item1, bool tranHole, bool tranHoleFivSix)
        {
            int _item0Hole1 = item0.Hole1;
            int _item0Hole2 = item0.Hole2;
            int _item0Hole3 = item0.Hole3;
            int _item0Hole4 = item0.Hole4;
            int _item0Hole5 = item0.Hole5;
            int _item0Hole6 = item0.Hole6;
            int _item0Hole5Exp = item0.Hole5Exp;
            int _item0Hole5Level = item0.Hole5Level;
            int _item0Hole6Exp = item0.Hole6Exp;
            int _item0Hole6Level = item0.Hole6Level;
            int _item0AttackCompose = item0.AttackCompose;
            int _item0DefendCompose = item0.DefendCompose;
            int _item0AgilityCompose = item0.AgilityCompose;
            int _item0LuckCompose = item0.LuckCompose;
            int _item0StrengthenLevel = item0.StrengthenLevel;

            int _item1Hole1 = item1.Hole1;
            int _item1Hole2 = item1.Hole2;
            int _item1Hole3 = item1.Hole3;
            int _item1Hole4 = item1.Hole4;
            int _item1Hole5 = item1.Hole5;
            int _item1Hole6 = item1.Hole6;
            int _item1Hole5Exp = item1.Hole5Exp;
            int _item1Hole5Level = item1.Hole5Level;
            int _item1Hole6Exp = item1.Hole6Exp;
            int _item1Hole6Level = item1.Hole6Level;
            int _item1AttackCompose = item1.AttackCompose;
            int _item1DefendCompose = item1.DefendCompose;
            int _item1AgilityCompose = item1.AgilityCompose;
            int _item1LuckCompose = item1.LuckCompose;
            int _item1StrengthenLevel = item1.StrengthenLevel;

            if (tranHole)
            {
                if (item0.Hole1 >= 0 || item1.Hole1 >= 0)
                    item1.Hole1 = _item0Hole1;
                    item0.Hole1 = _item1Hole1;
                if (item0.Hole2 >= 0 || item1.Hole2 >= 0)
                    item1.Hole2 = _item0Hole2;
                    item0.Hole2 = _item1Hole2;
                if (item0.Hole3 >= 0 || item1.Hole3 >= 0)
                    item1.Hole3 = _item0Hole3;
                    item0.Hole3 = _item1Hole3;
                if (item0.Hole4 >= 0 || item1.Hole4 >= 0)
                    item1.Hole4 = _item0Hole4;
                    item0.Hole4 = _item1Hole4;
            }
            if (tranHoleFivSix)
            {
                if (item0.Hole5 >= 0 || item1.Hole5 >= 0)
                    item1.Hole5 = _item0Hole5;
                    item0.Hole5 = _item1Hole5;
                if (item0.Hole6 >= 0 || item1.Hole6 >= 0)
                    item1.Hole6 = _item0Hole6;
                    item0.Hole6 = _item1Hole6;
                
            }
            //if (item0.Hole5Exp > 0 || item1.Hole5Exp > 0)
            item1.Hole5Exp = _item0Hole5Exp;
            item0.Hole5Exp = _item1Hole5Exp;
            item1.Hole5Level = _item0Hole5Level;
            item0.Hole5Level = _item1Hole5Level;
            //if (item0.Hole6Exp > 0 || item1.Hole6Exp > 0)
            item1.Hole6Exp = _item0Hole6Exp;
            item0.Hole6Exp = _item1Hole6Exp;
            item1.Hole6Level = _item0Hole6Level;
            item0.Hole6Level = _item1Hole6Level;

            item0.StrengthenLevel = _item1StrengthenLevel;
            item1.StrengthenLevel = _item0StrengthenLevel;
            item0.AttackCompose = _item1AttackCompose;
            item1.AttackCompose = _item0AttackCompose;
            item0.DefendCompose = _item1DefendCompose;
            item1.DefendCompose = _item0DefendCompose;
            item0.LuckCompose = _item1LuckCompose;
            item1.LuckCompose = _item0LuckCompose;
            item0.AgilityCompose = _item1AgilityCompose;
            item1.AgilityCompose = _item0AgilityCompose;
            if(item0.IsBinds)
                item1.IsBinds = item0.IsBinds;
            if (item1.IsBinds)
                item0.IsBinds = item1.IsBinds;
            //item.ValidDate = Item.ValidDate;
        }
    }
}
