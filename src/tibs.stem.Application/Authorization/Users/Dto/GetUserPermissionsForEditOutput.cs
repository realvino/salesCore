using System.Collections.Generic;
using tibs.stem.Authorization.Permissions.Dto;

namespace tibs.stem.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}