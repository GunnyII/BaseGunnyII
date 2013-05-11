using System;
using System.Collections.Generic;
using System.Text;

namespace SqlDataProvider.Data
{
    /// <summary>
    /// 箱子掉落表
    /// </summary>
    public class ShopGoodsShowListInfo : DataObject
    {
        public int Type { get; set; }
        public int ShopId { get; set; }
       
    }
}
