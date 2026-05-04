using Evento_Front_end.DTOs;
using Evento_Front_end.Models;

namespace Evento_Front_end.ViewModels.Company
{
    public class RequestsVM
    {
        public RequestDTO Request { get; set; }
        public IList<RequestDTO> Requests{ get; set; } = new List<RequestDTO>();
    }
}
