using Demo.Data;
using Demo.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Model
{
	[Serializable]
	public class BaseIndexModel<T> where T : BaseModel
    {
	    public BaseIndexModel()
	    {
	        Paging = new PaginationModel();
	    }

		[NonSerialized]
		public List<T> Results = new List<T>();

        public PaginationModel Paging { get; set; }

        public string Sort { get; set; }

        public void PopulateCreatedUser(List<Reference> users)
        {
            foreach (var item in Results)
            {
                if (item.CreatedUserId == Constants.SystemUserId)
                {
                    item.CreatedUser = "System";
                }
                else
                {
                    item.CreatedUser = users.Where(a => a.Id == item.CreatedUserId).Select(a => a.Name).FirstOrDefault();
                }
            }
        }
    }
}
