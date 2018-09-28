using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.Milestones.Dto;

namespace tibs.stem.MileStones.Dto
{
    public class GetMileStone 
    {
        public CreateMileInputDto mileStones { get; set; }
        public MileStoneDetailListDto[] mileStoneStatus { get; set; }
    }
}
