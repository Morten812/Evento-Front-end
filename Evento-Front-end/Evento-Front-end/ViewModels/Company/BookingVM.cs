using Evento_Front_end.DTOs;

namespace Evento_Front_end.ViewModels.Company
{
    public class BookingVM
    {
        public IList<ServiceDTO> Services { get; set; } = new List<ServiceDTO>();
    }
}
