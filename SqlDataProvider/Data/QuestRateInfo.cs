using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDataProvider.Data
{
    #region 任务条件
    public class QuestRateInfo
    {
        #region 任务条件

        public string BindMoneyRate { get; set; }
        public string ExpRate { get; set; }
        public string GoldRate { get; set; }
        public string ExploitRate { get; set; }
        /// <summary>
        /// 条件类型
        /// </summary>
        public int CanOneKeyFinishTime { get; set; }
        
        
        #endregion Model
    }
    #endregion
}
