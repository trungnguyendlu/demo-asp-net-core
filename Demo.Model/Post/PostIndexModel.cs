using Demo.Model.Category;

namespace Demo.Model.Post
{
    public class PostIndexModel : BaseIndexModel<PostModel>
    {
        public CategoryModel CurrentCategory { get; set; }
        public RightBarModel RightBar { get; set; } = new RightBarModel();
    }
}
