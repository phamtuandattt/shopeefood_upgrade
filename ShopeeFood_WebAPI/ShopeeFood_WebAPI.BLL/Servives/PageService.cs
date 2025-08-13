using AutoMapper;
using ShopeeFood_WebAPI.BLL.Dtos.PageDtos;
using ShopeeFood_WebAPI.BLL.IServices;
using ShopeeFood_WebAPI.DAL.IRepositories;
using ShopeeFood_WebAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopeeFood_WebAPI.BLL.Servives
{
    public class PageService : IPageServices
    {
        private readonly IRepository<PageSetting> repository;
        private readonly IMapper mapper;

        public PageService(IRepository<PageSetting> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<PageSettingDto>> GetPages()
        {
            var data = await repository.GetAllAsync();
            var result = mapper.Map<List<PageSettingDto>>(data);
            return result ?? new List<PageSettingDto>();
        }
    }
}
