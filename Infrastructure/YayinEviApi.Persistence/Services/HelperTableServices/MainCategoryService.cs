//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using YayinEviApi.Application.Abstractions.Services.ICategoryServices;
//using YayinEviApi.Application.Repositories;
//using YayinEviApi.Application.RequestParameters;
//using YayinEviApi.Domain.Entities.CategoriesE;

//namespace YayinEviApi.Persistence.Services.HelperTableServices
//{
//    public class MainCategoryService : IMainCategoryService
//    {
//        readonly IGetNewCode<MainCategory> _repository;

//        public MainCategoryService(IGetNewCode<MainCategory> repository)
//        {
//            _repository = repository;
//        }

//        public async Task<MainCategory> Add(MainCategory entity)
//        {

//            await _repository.AddAsync(entity);
//            await _repository.SaveAsync();
//            return entity;
//        }

//        public async Task Delete(string id)
//        {
//           await _repository.RemoveAsync(id);
//           await  _repository.SaveAsync();
//        }

//        public Task<MainCategory> Edit(MainCategory entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<IEnumerable<MainCategory>> GetAll(Pagination? pagination)
//        {
//            throw new NotImplementedException();
//        }

//        public Task<MainCategory> GetByIdAsync(string? id)
//        {
//            throw new NotImplementedException();
//        }

//        public int TotalCount()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
