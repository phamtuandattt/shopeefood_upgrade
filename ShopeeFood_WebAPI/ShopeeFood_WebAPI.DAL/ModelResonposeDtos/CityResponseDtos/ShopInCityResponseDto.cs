using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos
{
    public class ShopInCityResponseDto
    {
        [Key]
        public int CityID { get; set; }
        public int FieldID { get; set; }
        public int ShopID { get; set; }
        public string? ShopName { get; set; }
        public string? ShopImage { get; set; }
        public string? ShopAddress { get; set; }
        public string? ShopUptime { get; set; } 
        public int TotalRecords { get; set; }
    }
}
