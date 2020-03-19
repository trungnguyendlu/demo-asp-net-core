using System;
using System.Collections.Generic;

namespace Demo.Data
{
	[Serializable]
	public class BaseFindResponse<T> where T : class
	{
		public BaseFindResponse()
		{
			Results = new List<T>();
		}
        
		public List<T> Results { get; set; }

		/// <summary>
		/// only valid when pageoption is valid
		/// </summary>
		public long TotalRecords { get; set; }
	}
}
