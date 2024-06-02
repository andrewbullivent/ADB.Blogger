namespace ADB.Blogger.Domain.Models
{
    public record ListContext
    {
        public int Index { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public string[] OrderColumns { get; set; } = [];
        public int TotalCount { get; set; }

        public static ListContext DefaultListContext
        {
            get => new ()
            {
                Index = 0,
                PageSize = 20,
                TotalCount = 0
            };
        }

        public ListContext? NextPage()
        {

            if ((Index + 1) * PageSize > TotalCount)
            {
                return null;
            }

            return new ListContext
            {
                Index = this.Index++,
                PageSize = this.PageSize,
                OrderColumns = this.OrderColumns,
                TotalCount = this.TotalCount,
                SearchTerm = this.SearchTerm
            };
        }

        public ListContext? PreviousPage()
        {
            if (this.Index == 0)
            {
                return null;
            }

            return new ListContext
            {
                Index = this.Index - 1,
                PageSize = this.PageSize,
                OrderColumns = OrderColumns,
                TotalCount = this.TotalCount,
                SearchTerm = this.SearchTerm
            };
        }
    }
}
