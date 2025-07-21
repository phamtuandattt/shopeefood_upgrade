using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.DAL.ModelResonposeDtos.CityResponseDtos
{
    public class CityBusinessFieldResponseDto
    {
        [Key]
        public int FieldId { get; set; }
        public string? FieldName { get; set; }
    }
}
