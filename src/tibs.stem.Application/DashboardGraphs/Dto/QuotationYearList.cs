using System;
using System.Collections.Generic;
using System.Text;

namespace tibs.stem.DashboardGraphs.Dto
{
    public class QuotationYearList
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string MonthString { get; set; }
        public double Value { get; set; }
    }
    public class WonTarget
    {
        public int Year { get; set; }
        public QuotationYearList[] QyearList { get; set; }
    }

}
