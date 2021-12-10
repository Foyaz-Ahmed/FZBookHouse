using System;
using System.Collections.Generic;
using System.Text;

namespace FZBookHouse.Utilities
{
    public static class SD
    {
        public const string Proc_CoverType_Create = "usp_CreateCoverType";
        public const string Proc_CoverType_Get = "usp_GetCoverType";
        public const string Proc_CoverType_Get_All = "usp_GetCoverTypes";
        public const string Proc_CoverType_Update = "usp_UpdateCoverType";
        public const string Proc_CoverType_Delete = "usp_DeleteCoverType";

        public const string Role_User_Indiv = "Individual Customer";
        public const string Role_User_Company = "Company Customer";
        public const string Role__Admin = "Admin";
        public const string Role__Emp = "Employee";
    }
}
