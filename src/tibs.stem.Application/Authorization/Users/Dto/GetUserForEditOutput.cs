using System;
using System.Collections.Generic;
using Abp.Authorization.Users;
using Abp.Organizations;
using tibs.stem.Organizations.Dto;

namespace tibs.stem.Authorization.Users.Dto
{
    public class GetUserForEditOutput
    {
        public Guid? ProfilePictureId { get; set; }

        public UserEditDto User { get; set; }

        public UserRoleDto[] Roles { get; set; } 

        public UserRootDto Root { get; set; }

        public List<OrganizationUnitDto> OrganizationUnits { get; set; }
    }
}