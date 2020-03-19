using Demo.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.Model.Admin.Media
{
    [Serializable]
    public class MediaEditModel : BaseEditModel<MediaModel>
    {
        [Display(Name = "Phân Loại")]
        public MediaType MediaType { get; set; }
    }
}