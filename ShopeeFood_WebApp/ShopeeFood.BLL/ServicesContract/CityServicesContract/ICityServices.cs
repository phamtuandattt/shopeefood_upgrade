using ShopeeFood.BLL.DTOS.CityDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood.BLL.ServicesContract.CityServicesContract
{
    public interface ICityServices
    {
        IEnumerable<CityDto> GetAll();
    }
}
