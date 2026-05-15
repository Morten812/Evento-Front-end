using Evento_Front_end.DTOs;

namespace Evento_Front_end.Models
{
    public class Request
    {
        public int RequestID { get; set; }
        public int ServiceID { get; set; }
        public int CompanyID { get; set; }
        public int CustomerID { get; set; }
        public string Description { get; set; }
        public RequestStatus Status { get; set; }
        public Service Service { get; set; }
        public Customer Customer { get; set; }
    }

    public enum RequestStatus
    {
        Pending,
        Approved,
        Cancelled,
        Rejected
    }
}
