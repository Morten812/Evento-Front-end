using Evento_Front_end.Models;

namespace Evento_Front_end.ViewModels.Company
{
    public class RequestRowVM
    {
        public int RequestID { get; set; }
        public int ServiceID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
    }
}
