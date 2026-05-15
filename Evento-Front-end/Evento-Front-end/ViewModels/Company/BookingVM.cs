using Evento_Front_end.DTOs;

namespace Evento_Front_end.ViewModels.Company
{
    public class BookingVM
    {
        public int CustomerID { get; set; }
        public int CompanyID { get; set; }
        public IList<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
        public IList<CustomerDTO> Customers { get; set; } = new List<CustomerDTO>();
    }
}
