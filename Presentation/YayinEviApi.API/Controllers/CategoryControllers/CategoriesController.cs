using Microsoft.AspNetCore.Mvc;
using YayinEviApi.Application.DTOs.CategoriesDtos;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities.CategoriesE;
using YayinEviApi.Infrastructure.Operations;

namespace YayinEviApi.API.Controllers.CategoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        readonly IGetNewCode<MainCategory> _mainCategoryService;
        readonly IGetNewCode<SubCategory_1> _sub_1CategoryService;
        readonly IGetNewCode<SubCategory_2> _sub_2CategoryService;
        readonly IGetNewCode<SubCategory_3> _sub_3CategoryService;
        readonly IGetNewCode<SubCategory_4> _sub_4CategoryService;
        public CategoriesController(IGetNewCode<MainCategory> mainCategoryService, IGetNewCode<SubCategory_1> sub_1CategoryService, IGetNewCode<SubCategory_2> sub_2CategoryService, IGetNewCode<SubCategory_3> sub_3CategoryService, IGetNewCode<SubCategory_4> sub_4CategoryService)
        {
            _mainCategoryService = mainCategoryService;
            _sub_1CategoryService = sub_1CategoryService;
            _sub_2CategoryService = sub_2CategoryService;
            _sub_3CategoryService = sub_3CategoryService;
            _sub_4CategoryService = sub_4CategoryService;
        }

        //--------------------------------------MAINCATEGORY-------------------------------------------

        [HttpPost("addMainCategory")]
        public async Task<IActionResult> Add(MainCategoryDto entity)
        {
            var category = entity.EntityCovert<MainCategory>();
            await _mainCategoryService.AddAsync(category);
            entity.Id = category.Id.ToString();

            await _mainCategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpPost("editMainCategory")]
        public async Task<IActionResult> Edit(MainCategoryDto entity)
        {
            var cat = entity.EntityCovert<MainCategory>();
            _mainCategoryService.Update(cat);
            await _mainCategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpGet("listMainCategory")]
        public async Task<IActionResult> List(string? mainId)
        {
            var result = _mainCategoryService.GetAll().ToList();
            
            return Ok(result.EntityListConvert<MainCategoryDto>());
        }


        //--------------------------------------SUBCATEGORY_1-------------------------------------------

        [HttpPost("addSubCategory_1")]
        public async  Task<IActionResult> Add(SubCategory_1Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_1>();
            await _sub_1CategoryService.AddAsync(category);
            entity.Id = category.Id.ToString();
            await _sub_1CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpPut("editSubCategory_1")]
        public async Task<IActionResult> Edit(SubCategory_1 entity)
        {
            var category = entity.EntityCovert<SubCategory_1>();
            _sub_1CategoryService.Update(category);
            await _sub_1CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpGet("listSubCategory_1")]
        public async Task<IActionResult> ListSub_1(string? id)
        {
            
            var result = _sub_1CategoryService.GetWhere(x => x.Id.ToString() == id).ToList();

            return Ok(result.EntityListConvert<SubCategory_1>());
        }

        //--------------------------------------SUBCATEGORY_2-------------------------------------------

        [HttpPost("addSubCategory_2")]
        public async Task<IActionResult> Add(SubCategory_2Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_2>();
            await _sub_2CategoryService.AddAsync(category);
            entity.Id = category.Id.ToString();
            await _sub_2CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpPut("editSubCategory_2")]
        public async Task<IActionResult> Edit(SubCategory_2Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_2>();
            _sub_2CategoryService.Update(category);
            await _sub_2CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpGet("listSubCategory_2")]
        public async Task<IActionResult> ListSub_2(string? id)
        {

            var result = _sub_2CategoryService.GetWhere(x => x.Id.ToString() == id).ToList();

            return Ok(result.EntityListConvert<SubCategory_2>());
        }

        //--------------------------------------SUBCATEGORY_3-------------------------------------------

        [HttpPost("addSubCategory_3")]
        public async Task<IActionResult> Add(SubCategory_3Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_3>();
            await _sub_3CategoryService.AddAsync(category);
            entity.Id = category.Id.ToString();
            await _sub_3CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpPut("editSubCategory_3")]
        public async Task<IActionResult> Edit(SubCategory_3Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_3>();
            _sub_3CategoryService.Update(category);
            await _sub_3CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpGet("listSubCategory_3")]
        public async Task<IActionResult> ListSub_3(string? id)
        {

            var result = _sub_3CategoryService.GetWhere(x => x.Id.ToString() == id).ToList();

            return Ok(result.EntityListConvert<SubCategory_3>());
        }

        //--------------------------------------SUBCATEGORY_3-------------------------------------------

        [HttpPost("addSubCategory_4")]
        public async Task<IActionResult> Add(SubCategory_4Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_4>();
            await _sub_4CategoryService.AddAsync(category);
            entity.Id = category.Id.ToString();
            await _sub_4CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpPut("editSubCategory_4")]
        public async Task<IActionResult> Edit(SubCategory_4Dto entity)
        {
            var category = entity.EntityCovert<SubCategory_4>();
            _sub_4CategoryService.Update(category);
            await _sub_4CategoryService.SaveAsync();
            return Ok(entity);
        }
        [HttpGet("listSubCategory_4")]
        public async Task<IActionResult> ListSub_4(string? id)
        {

            var result = _sub_3CategoryService.GetWhere(x => x.Id.ToString() == id).ToList();

            return Ok(result.EntityListConvert<SubCategory_4>());
        }

    }
}
