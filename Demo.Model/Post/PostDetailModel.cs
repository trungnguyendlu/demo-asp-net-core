using System.Collections.Generic;

namespace Demo.Model.Post
{
    public class PostDetailModel : BaseEditModel<PostModel>
    {
        public List<PostModel> RelatedPosts { get; set; }
        public RightBarModel RightBar { get; set; } = new RightBarModel();
    }
}
