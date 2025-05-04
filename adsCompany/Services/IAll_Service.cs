using adsCompany.Services;
using adsCompany.DbContexts;
using adsCompany.Entities;
using adsCompany.DTO;
using adsCompnay.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace adsCompany.Services
{
    public interface IAll_Service
    {
     public IQueryable<ProductionDTO> GetProductionList(int id=0,int pageRow=5,int pageNumber = 0);
     public Response AddProduction(ProductionDTO productionDTO);
     public IQueryable<AdDTO> GetAdList(int id = 0, int pageRow = 5, int pageNumber = 0);
     public Response AddAd(AdDTO adDTO);
     
    }
}