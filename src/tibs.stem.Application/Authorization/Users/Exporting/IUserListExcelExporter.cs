using System.Collections.Generic;
using tibs.stem.Authorization.Users.Dto;
using tibs.stem.Dto;

namespace tibs.stem.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}