using Demo.Data;
using System;
using System.Collections.Generic;

namespace Demo.Model.Admin.Post
{
    [Serializable]
    public class PostEditModel : BaseEditModel<PostModel>
    {
        public List<Reference> Categories { get; set; }
    }
}