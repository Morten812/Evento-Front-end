using Evento_Front_end.Models;

namespace Evento_Front_end.DTOs
{
    public class RequestDTO
    {
        public int RequestID { get; set; }
        public int ServiceID { get; set; }
        public int CompanyID { get; set; }
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
    }
    public enum RequestStatus
    {
        Pending,
        Approved,
        Cancelled,
        Rejected
    }
}
