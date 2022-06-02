namespace AndriiYefimov.SayolloHW2.Models
{
    public class PurchaseItemModel
    {
        public string title { get; set; }
        public string item_id { get; set; }
        public string item_name { get; set; }
        public string item_image { get; set; }
        public double price { get; set; }
        public string currency { get; set; }
        public string currency_sign { get; set; }
        public string status { get; set; }
        public int error_code { get; set; }
    }
}