namespace CrudProduct.Models
{
    public partial class Category
    {
        public Category() 
        {
            Products = new List<Product>();
        }
        public string CtCode { get; set; }
        public string CatName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
