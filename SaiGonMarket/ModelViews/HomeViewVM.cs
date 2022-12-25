using SaiGonMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaiGonMarket.ModelViews
{
    public class HomeViewVM
    {
        public List<ProductHomeVM> Products { get; set; }
        public List<TblTinTuc> TinTucs { get; set; }
        public QuangCao quangcao { get; set; }
    }
}
