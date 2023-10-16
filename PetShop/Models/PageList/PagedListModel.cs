namespace PetShop.Models.PageList
{
    public class PagedListModel<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; }
    }
}
