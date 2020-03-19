using System;
using Demo.Util;

namespace Demo.Model
{
	[Serializable]
	public class PaginationModel
	{
		public PaginationModel()
		{
			ShowPaging = true;
			PageSize = Constants.DefaultPageSize;
            PageNumber = 1;
		}

		public long TotalRecords { get; set; }

		public int PageSize { get; set; }

		public int PageNumber { get; set; }

        public int TotalPages { get; set; }

		public int DisplayedPageStartIndex { get; private set; }

		public int DisplayedPageEndIndex { get; private set; }

		public string PageClickUrl { get; set; }

		public bool ShowPaging { get; set; }

		public string PaginationId { get; set; }

        public bool IsTransaction { get; set; }

        public bool IsTraceForMe { get; set; }

		/// <summary>
		/// Format xxxx({0}). PageClickFunction ~ xxx, param {0} is page index and it must be require.
		///  </summary>
		public string PageClickFunction { get; set; }

		/// <summary>
		/// Format xxxx(0}). PageSizeClickFunction ~ xxx, param {0} is HTMLSelectElement.
		///  </summary>
		public string PageSizeClickFunction { get; set; }

		public void CalculatePaging()
		{
			TotalPages = (int)(Math.Ceiling(TotalRecords / (double)PageSize));
			PageNumber = PageNumber <= 0 ? 1 : (PageNumber > TotalPages ? TotalPages : PageNumber);

			DisplayedPageStartIndex = (PageNumber - Constants.DefaultPageCount / 2) < 1
										? 1
										: PageNumber - Constants.DefaultPageCount / 2;

			DisplayedPageEndIndex = (PageNumber + Constants.DefaultPageCount / 2) > TotalPages
										? TotalPages
										: PageNumber + Constants.DefaultPageCount / 2;
		}

		public string GetExecutePageClick(int pageIndex, bool alwaysSetEmptyFunction = false)
		{
			if (!alwaysSetEmptyFunction)
			{
				pageIndex = pageIndex <= 0 ? 1 : (pageIndex > TotalPages ? TotalPages : pageIndex);

				if (!string.IsNullOrEmpty(PageClickFunction))
				{
					return $"onclick=javascript:{string.Format(PageClickFunction, pageIndex)}";
				}

				if (!string.IsNullOrEmpty(PageClickUrl))
				{
					return $"class=fwLink href=javascript:void(0) link={PageClickUrl}{(PageClickUrl.IndexOf("?", StringComparison.Ordinal) >= 0 ? "&" : "?")}{Constants.DefaultPageQueryName}={pageIndex}";
				}
			}

			return "href=javascript:void(0)";
		}
	}
}
