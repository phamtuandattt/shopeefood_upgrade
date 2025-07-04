using ShopeeFood.BLL.DTOS.BusinessDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.BusinessServicesContract
{
    public interface IBusinessServices
    {
        IEnumerable<BusinessDto> GetAllByCity(int cityId);
    }
}
