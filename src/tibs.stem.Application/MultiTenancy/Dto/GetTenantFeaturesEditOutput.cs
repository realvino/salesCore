using System.Collections.Generic;
using Abp.Application.Services.Dto;
using tibs.stem.Editions.Dto;

namespace tibs.stem.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}