using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tibs.stem.ActivityCommands;
using tibs.stem.ActivityCommandss.Dto;

namespace tibs.stem.ActivityCommandss
{
   public class ActivityCommandAppService : stemAppServiceBase, IActivityCommandAppService
    {

        public readonly IRepository<ActivityCommand> _ActivityCommandRepository;
        public ActivityCommandAppService(IRepository<ActivityCommand> ActivityCommandRepository)
        {
            _ActivityCommandRepository = ActivityCommandRepository;
        }
        public ListResultDto<ActivityCommandListDto> GetActivityCommand(GetActivityCommandInput input)
        {
           
                var command = _ActivityCommandRepository.GetAll().Where(p => p.ActivityId == input.ActivityId)
                 .WhereIf(
                        !input.Filter.IsNullOrEmpty(),
                         p => p.Commands.Contains(input.Filter)) 
                .ToList();

                return new ListResultDto<ActivityCommandListDto>(command.MapTo<List<ActivityCommandListDto>>());
           
        }
        public async Task CreateActivityCommand(CreateActivityCommandInput input)
        {
            var command = input.MapTo<ActivityCommand>();

            await _ActivityCommandRepository.InsertAsync(command);
            
            
        }

        public async Task DeleteActivityCommand(EntityDto input)
        {
            await _ActivityCommandRepository.DeleteAsync(input.Id);
        }
    }
}
