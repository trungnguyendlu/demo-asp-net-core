using System;
using System.Collections.Generic;
using Demo.Data;

namespace Demo.Model.Admin.User
{
    [Serializable]
    public class UserEditModel : BaseEditModel<UserModel>
    {
        public List<Reference> Roles { get; set; }
    }
}