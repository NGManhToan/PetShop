﻿namespace PetShop.Models.PageList
{
    public class PagedListModel<T>
    {
        public List<T> Items { get; set; }
        public List<T> itemHome { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }



		public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }

}
