using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKSLK.API.Enum
{
    public enum StatusCode
    {
        InsertUpdateSuccess = 201,
        GetDataSuccess = 200,
        GetDataNoContent = 204,
        ExistsData = 999,
        BadRequest = 400,
        Exeption = 500,
    }
}
