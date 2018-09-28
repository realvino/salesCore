using System;
using System.Collections.Generic;
using System.Text;
using tibs.stem.Authorization.Users.Dto;

namespace tibs.stem.Authorization.Users.Dto
{
   public class GetTarget
    {
        public CreateTargetInput Targets { get; set; }
        public GetAvailableTarget TargetDetail { get; set; }

    }
    public class GetAvailableTarget
    {
        public decimal Totaltarget { get; set; }
        public decimal AvailableTarget { get; set; }
    }

    public class GetTargetEditInput
    {
        public int Id { get; set; }
        public DateTime DateInput { get; set; }
    }
}
