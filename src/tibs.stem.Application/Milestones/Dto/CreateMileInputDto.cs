using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.MileStones;

namespace tibs.stem.MileStones.Dto
{
    [AutoMap(typeof(MileStone))]
    public class CreateMileInputDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public int Id { get; set; }
        public virtual bool IsQuotation { get; set; }
        public virtual bool EndOfQuotation { get; set; }

    }
}
