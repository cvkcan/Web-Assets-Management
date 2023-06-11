namespace Web_Assets_Management.Models
{
    public class Categories
    {
        public int CategorieId { get; set; }
        public string CategorieName { get; set; }
        public Categories()
        {

        }
        public Categories(int id, string name)
        {
            CategorieId = id;
            CategorieName = name;
        }
    }
}
