using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.PriceLevels;

namespace tibs.stem.PriceLevelss.Dto
{
    [AutoMap(typeof(PriceLevel))]
    public class PriceLevelCreate
    {
        public int Id { get; set; }
        public string PriceLevelCode { get; set; }
        public string PriceLevelName { get; set; }
        public int TenantId { get; set; }
    }
    [AutoMapFrom(typeof(PriceLevel))]
    public class PriceLevelList
    {
        public int Id { get; set; }
        public string PriceLevelCode { get; set; }
        public string PriceLevelName { get; set; }
        public int TenantId { get; set; }
    }
    public class GetPriceLevelListInput
    {
        public string Filter { get; set; }
    }
    public class GetPriceLevel
    {
        public PriceLevelCreate GetPriceLevels { get; set; }
    }
}
