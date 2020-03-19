using System.Collections.Generic;

namespace Corporation.Models.Admin.Theme
{
    public class ResourceModel
    {
        public List<string> Fonts { get; set; }
        public List<string> Css { get; set; }
        public List<string> Js { get; set; }
        public string BodyClass { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
    }
}
