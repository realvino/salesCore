using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.DashboardGraphs
{
    public interface IDashboardGraphAppService : IApplicationService
    {
        Task<Array> WonTargetDevelopment(NullableIdDto input);
        Task<Array> GetActivepotential(NullableIdDto input);

    }
}
