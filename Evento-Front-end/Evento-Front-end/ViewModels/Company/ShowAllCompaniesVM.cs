

namespace Evento_Front_end.ViewModels.Company;

using Evento_Front_end.Models;
using Evento_Front_end.DTOs;

public class ShowAllCompaniesVM
{
    public IList<CompanyDTO> Companies { get; set; } = new List<CompanyDTO>();


    public List<string> SelectedMunicipalities { get; set; } = new List<string>();
    public List<string> SelectedServices { get; set; } = new List<string>();

}
