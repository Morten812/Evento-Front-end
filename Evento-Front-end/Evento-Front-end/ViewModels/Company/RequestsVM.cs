using Evento_Front_end.DTOs;
using Evento_Front_end.Models;

namespace Evento_Front_end.ViewModels.Company
{
    public class RequestsVM
    {
        public IList<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();
        public RequestDTO Request { get; set; }
        public IList<RequestDTO> Requests { get; set; } = new List<RequestDTO>();
        public List<RequestRowVM> RequestRows { get; set; } = new();
    }
}
