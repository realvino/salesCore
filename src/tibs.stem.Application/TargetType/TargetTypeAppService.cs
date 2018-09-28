using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tibs.stem.TargetType.Dto;
using tibs.stem.TargetTypess;

using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;

namespace tibs.stem.TargetType
{
    public  class TargetTypeAppService : stemAppServiceBase, ITargetTypeAppService
    {
        public readonly IRepository<TargetTypes> _TargetTypeRepository;       
        public TargetTypeAppService(IRepository<TargetTypes> TargetTypeRepository) 
        {
            _TargetTypeRepository = TargetTypeRepository;
        }
        public ListResultDto<TargetTypeListDto> GetTargetType(GetTargetTypeInput input)
        {
           
                var TargetType = _TargetTypeRepository.GetAll()
                 .WhereIf(
                 !input.Filter.IsNullOrEmpty(),
                 p => p.Name.Contains(input.Filter) ||
                     p.Code.Contains(input.Filter)
                 );


                var query = from s in TargetType
                            select new TargetTypeListDto
                            {
                                Code = s.Code,
                                Name = s.Name,
                                Id = s.Id
                            };
                var list = query.ToList();
                return new ListResultDto<TargetTypeListDto>(list.MapTo<List<TargetTypeListDto>>());
          
        }
        public async Task<PagedResultDto<TargetTypeListDto>> GetTargetType1(GetTargetTypeInput1 input)
        {
            var TargetType = _TargetTypeRepository.GetAll()
             .WhereIf(
             !input.Filter.IsNullOrEmpty(),
             p => p.Name.Contains(input.Filter) ||
                 p.Code.Contains(input.Filter)
             );
            var query = (from s in TargetType
                        select new TargetTypeListDto
                        {
                            Code = s.Code,
                            Name = s.Name,
                            Id = s.Id
                        });

            var TargettypeCount = await query.CountAsync();
            var Targettypelist = await query
                .OrderBy(input.Sorting)
                .PageBy(input)
                .ToListAsync();

            var Targettypelistoutput = Targettypelist.MapTo<List<TargetTypeListDto>>();

            return new PagedResultDto<TargetTypeListDto>(TargettypeCount, Targettypelistoutput);

        }

        public async Task<GetTargetType> GetTargetTypeForEdit(EntityDto input)
        {
            var output = new GetTargetType();
            try
            {
                if (input.Id > 0)
                {
                    var query = _TargetTypeRepository
                                   .GetAll().Where(p => p.Id == input.Id).FirstOrDefault();

                    output.TargetTypes = query.MapTo<CreateTargetTypeInput>();

                }
            }
            catch (Exception obj)
            {
                string ff = obj.Message.ToString();
            }
            return output;

        }

        public async Task CreateOrUpdateTargetType(CreateTargetTypeInput input)
        {
            
                if (input.Id == 0)
                {
                    await CreateTargetType(input);
                }
                else
                {
                    await UpdateTargetType(input);
                }
        }

        public virtual async Task CreateTargetType(CreateTargetTypeInput input)
        {

            
                var TargetType = input.MapTo<TargetTypes>();

                var query = _TargetTypeRepository.GetAll().Where(p => p.Code == input.Code && p.Name == input.Name).FirstOrDefault();
                if (query == null)
                {
                    await _TargetTypeRepository.InsertAsync(TargetType);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Target Type ...");
                }
          
        }

        public virtual async Task UpdateTargetType(CreateTargetTypeInput input)
        {
                var TargetType = input.MapTo<TargetTypes>();

                var query = _TargetTypeRepository.GetAll()
                    .Where(p => p.Code == input.Code && p.Name == input.Name && p.Id != input.Id).FirstOrDefault();
                if (query == null)
                {
                    await _TargetTypeRepository.UpdateAsync(TargetType);
                }
                else
                {
                    throw new UserFriendlyException("Ooops!", "Duplicate Data Occured in Target Type ...");
                }           
        }

        public async Task GetDeleteTargetType(EntityDto input)
        {
            await _TargetTypeRepository.DeleteAsync(input.Id);
        }
    }

}
