namespace Evento_Front_end.DTOs
{
    public class ServiceDTO
    {
        public int ServiceID { get; set; }
        public int CompanyID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
