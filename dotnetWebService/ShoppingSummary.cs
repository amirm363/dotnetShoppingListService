namespace dotnetWebService
{
    using System.ComponentModel.DataAnnotations;
    public class ShoppingSummary
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; } 
        public string OrderDetails { get; set; }
    }
}
