namespace Web_Assets_Management.Models
{
    public class Assets
    {
        public int AssetsNumber { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string CategorieName { get; set; }
        public string Vendor { get; set; }
        public Assets()
        {

        }
        public Assets(int number,string name,int price,string categories,string vendor)
        {
            AssetsNumber = number;
            Name = name;
            Price = price;
            CategorieName= categories;
            Vendor = vendor;
        }
    }
}
