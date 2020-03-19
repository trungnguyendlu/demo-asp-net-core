using System;

namespace Demo.Data
{
	[Serializable]
	public class BaseFindRequest
	{
        private int _pageNumber = 1;
        private int _pageSize = 10;
        
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value > 1 ? value : 1; }
        }

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value > 1 ? value : 1; }
        }

        public string Sort { get; set; }

        public int Skip => (PageNumber - 1) * PageSize;
    }
}
